using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Serve static files (e.g., /uploads under wwwroot)
app.UseStaticFiles();


// In-memory articles store for demo purposes
var articles = new List<Article>();


// Create article
app.MapPost("/api/articles", (ArticleCreateRequest req) =>
{
    var article = new Article(
        Guid.NewGuid(),
        req.Title,
        req.Dir,
        req.ContentHtml,
        req.ContentJson,
        DateTime.UtcNow
    );
    articles.Add(article);
    return Results.Created($"/api/articles/{article.Id}", article);
});

// Get single article
app.MapGet("/api/articles/{id:guid}", (Guid id) =>
{
    var found = articles.FirstOrDefault(a => a.Id == id);
    return found is null ? Results.NotFound() : Results.Ok(found);
});

// List articles
app.MapGet("/api/articles", () => articles);

app.MapPost("/api/upload", async (HttpRequest request, IWebHostEnvironment env) =>
{
    if (!request.HasFormContentType)
        return Results.BadRequest("Expected multipart form-data");

    var form = await request.ReadFormAsync();
    var file = form.Files.FirstOrDefault();

    if (file == null || file.Length == 0)
        return Results.BadRequest("No file uploaded");

    // Save file locally (you could save to cloud storage in production)
    var webRoot = env.WebRootPath ?? Path.Combine(env.ContentRootPath, "wwwroot");
    var uploadsDir = Path.Combine(webRoot, "uploads");
    Directory.CreateDirectory(uploadsDir);

    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
    var filePath = Path.Combine(uploadsDir, fileName);

    await using var stream = File.Create(filePath);
    await file.CopyToAsync(stream);

    // Return public URL (make sure "wwwroot" is served as static files)
    var fileUrl = $"http://localhost:5076/uploads/{fileName}";
    return Results.Ok(new { url = fileUrl });
});

app.Run();


internal record Article(
    Guid Id,
    string Title,
    string Dir,
    string ContentHtml,
    JsonElement? ContentJson,
    DateTime CreatedAt
);

internal record ArticleCreateRequest(
    string Title,
    string Dir,
    string ContentHtml,
    JsonElement? ContentJson
);
