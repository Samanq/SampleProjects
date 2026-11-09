using System.Security.Claims;
using EntraExternalIdSample.Components;
using EntraExternalIdSample.Data;
using EntraExternalIdSample.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddSingleton<AccessCodeService>();
builder.Services.AddHttpClient<GraphService>();

// Entra External ID (CIAM) via OpenID Connect. All values come from the "AzureAd" section of appsettings.json.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    // Authorization code flow (+PKCE). The handler defaults to the implicit "id_token" flow,
    // which Entra rejects with AADSTS700054 unless implicit grant is enabled on the app registration.
    options.ResponseType = OpenIdConnectResponseType.Code;
    // Keep the raw id_token in the auth cookie so Profile can display it.
    options.SaveTokens = true;

    options.Events.OnRedirectToIdentityProvider = ctx =>
    {
        // "/authentication/signup" asks Entra to open the "Create account" page directly.
        if (ctx.Properties.Items.TryGetValue("prompt", out var prompt))
            ctx.ProtocolMessage.Prompt = prompt;
        return Task.CompletedTask;
    };

    options.Events.OnTokenValidated = async ctx =>
    {
        // First successful sign-in creates the PostgreSQL row; later sign-ins update it.
        var principal = ctx.Principal!;
        var oid = principal.FindFirstValue("http://schemas.microsoft.com/identity/claims/objectidentifier")
                  ?? principal.FindFirstValue("oid")
                  ?? principal.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var db = ctx.HttpContext.RequestServices.GetRequiredService<AppDbContext>();
        var user = await db.Users.SingleOrDefaultAsync(u => u.ObjectId == oid);
        if (user is null)
        {
            user = new AppUser { ObjectId = oid, FirstSeenUtc = DateTime.UtcNow };
            db.Users.Add(user);
        }
        user.Email = principal.FindFirstValue("preferred_username")
                     ?? principal.FindFirstValue(ClaimTypes.Email)
                     ?? principal.FindFirstValue("emails");
        user.DisplayName = principal.FindFirstValue("name") ?? principal.Identity?.Name;
        user.LastSeenUtc = DateTime.UtcNow;
        user.LoginCount++;
        await db.SaveChangesAsync();

        // Resolve the group ids in the "groups" claim to readable access_code claims (one Graph call per login).
        var groupIds = principal.FindAll("groups").Select(c => c.Value).ToList();
        if (groupIds.Count > 0)
        {
            var graph = ctx.HttpContext.RequestServices.GetRequiredService<GraphService>();
            var codes = ctx.HttpContext.RequestServices.GetRequiredService<AccessCodeService>();
            var names = await graph.GetGroupNamesAsync(groupIds);
            var identity = (ClaimsIdentity)principal.Identity!;
            foreach (var name in names.Values)
                if (codes.ByGroupName(name) is { } code)
                    identity.AddClaim(new Claim(AccessCode.ClaimType, code.Code));
        }
    };
});

// Signed in but missing the required access code -> this page instead of the default /Account/AccessDenied.
builder.Services.Configure<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme,
    o => o.AccessDeniedPath = "/access-denied");

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("WarehouseA", p => p.RequireClaim(AccessCode.ClaimType, "ADVCD01"))
    .AddPolicy("WarehouseB", p => p.RequireClaim(AccessCode.ClaimType, "ADVCD02"));
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
    await scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.EnsureCreatedAsync();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

var auth = app.MapGroup("/authentication");

auth.MapGet("/login", (string? returnUrl) =>
    TypedResults.Challenge(new AuthenticationProperties { RedirectUri = returnUrl ?? "/" }));

auth.MapGet("/signup", (string? returnUrl) =>
{
    var props = new AuthenticationProperties { RedirectUri = returnUrl ?? "/" };
    props.Items["prompt"] = "create";
    return TypedResults.Challenge(props);
});

auth.MapPost("/logout", ([FromForm] string? returnUrl) =>
    TypedResults.SignOut(
        new AuthenticationProperties { RedirectUri = returnUrl ?? "/" },
        [CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme]));

app.Run();
