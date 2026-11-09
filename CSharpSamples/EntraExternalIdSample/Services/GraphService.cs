using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EntraExternalIdSample.Services;

/// Minimal app-only Microsoft Graph client (client credentials). Needs the Graph application permission
/// Group.ReadWrite.All with admin consent on the app registration.
public class GraphService(HttpClient http, IConfiguration config)
{
    private const string Graph = "https://graph.microsoft.com/v1.0";
    private string? _token;
    private DateTimeOffset _tokenExpires;

    /// Returns the object id of the security group with this display name, creating it if needed.
    public async Task<string> EnsureGroupAsync(string displayName)
    {
        JsonElement found = await GetAsync($"{Graph}/groups?$filter=displayName eq '{displayName}'&$select=id");
        JsonElement existing = found.GetProperty("value").EnumerateArray().FirstOrDefault();

        if (existing.ValueKind == JsonValueKind.Object)
        {
            return existing.GetProperty("id").GetString()!;
        }

        JsonElement created = await SendAsync(HttpMethod.Post, $"{Graph}/groups", new
        {
            displayName,
            mailNickname = displayName,
            mailEnabled = false,
            securityEnabled = true,
        });
        return created.GetProperty("id").GetString()!;
    }

    /// Adds the user to the group. Already a member counts as success.
    /// A group created moments ago can 404 until the directory has replicated it, so 404 is retried.
    public async Task AddMemberAsync(string groupId, string userObjectId)
    {
        Dictionary<string, string> body = new Dictionary<string, string> { ["@odata.id"] = $"{Graph}/directoryObjects/{userObjectId}" };
        
        for (int attempt = 1; ; attempt++)
        {
            using HttpResponseMessage res = await RawSendAsync(HttpMethod.Post, $"{Graph}/groups/{groupId}/members/$ref", body);
            
            if (res.IsSuccessStatusCode)
            {
                return;
            }

            string text = await res.Content.ReadAsStringAsync();
            
            if (res.StatusCode == HttpStatusCode.BadRequest &&
                text.Contains("already exist", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            if (res.StatusCode == HttpStatusCode.NotFound && attempt < 6)
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
                continue;
            }
            throw new HttpRequestException($"Graph add member failed: {(int)res.StatusCode} {text}");
        }
    }

    /// Resolves group object ids (e.g. from the "groups" claim) to display names.
    public async Task<Dictionary<string, string>> GetGroupNamesAsync(IEnumerable<string> ids)
    {
        string[] list = ids.ToArray();
        
        if (list.Length == 0)
        {
            return [];
        }
        
        JsonElement res = await SendAsync(
            HttpMethod.Post,
            $"{Graph}/directoryObjects/getByIds",
            new { ids = list, types = new[] { "group" } });
        
        return res.GetProperty("value")
            .EnumerateArray()
            .ToDictionary(g => g
                .GetProperty("id")
                .GetString()!, g => g.GetProperty("displayName").GetString() ?? "");
    }

    private Task<JsonElement> GetAsync(string url)
    {
        return SendAsync(HttpMethod.Get, url, null);
    }

    private async Task<JsonElement> SendAsync(HttpMethod method, string url, object? body)
    {
        using HttpResponseMessage res = await RawSendAsync(method, url, body);
        
        if (!res.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Graph {method} {url} failed: {(int)res.StatusCode} {await res.Content.ReadAsStringAsync()}");
        }
        return await res.Content.ReadFromJsonAsync<JsonElement>();
    }

    private async Task<HttpResponseMessage> RawSendAsync(HttpMethod method, string url, object? body)
    {
        using HttpRequestMessage req = new HttpRequestMessage(method, url);
        
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync());
        
        if (body is not null)
        {
            req.Content = JsonContent.Create(body);
        }
        
        return await http.SendAsync(req);
    }

    private async Task<string> GetTokenAsync()
    {
        if (_token is not null && DateTimeOffset.UtcNow < _tokenExpires)
        {
            return _token;
        }

        IConfigurationSection ad = config.GetSection("AzureAd");
        
        // Authority is https://<sub>.ciamlogin.com/ ; the tenant for the token endpoint is <sub>.onmicrosoft.com
        Uri authority = new (ad["Authority"]!);
        string tenant = authority.Host.Split('.')[0] + ".onmicrosoft.com";
        string tokenUrl = $"{authority.GetLeftPart(UriPartial.Authority)}/{tenant}/oauth2/v2.0/token";

        using HttpResponseMessage res = await http.PostAsync(
            tokenUrl,
            new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = "client_credentials",
            ["client_id"] = ad["ClientId"]!,
            ["client_secret"] = ad["ClientSecret"]!,
            ["scope"] = "https://graph.microsoft.com/.default",
        }));

        if (!res.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Token request failed: {(int)res.StatusCode} {await res.Content.ReadAsStringAsync()}");
        }

        JsonElement json = await res.Content.ReadFromJsonAsync<JsonElement>();
        
        _token = json.GetProperty("access_token").GetString();
        
        _tokenExpires = DateTimeOffset.UtcNow
            .AddSeconds(json.GetProperty("expires_in").GetInt32() - 60);
        
        return _token!;
    }
}
