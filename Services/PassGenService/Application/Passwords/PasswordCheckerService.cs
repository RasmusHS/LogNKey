using System.Net.Http.Json;

namespace Application.Passwords;

public class PasswordCheckerService
{
    private readonly HttpClient _httpClient;

    public PasswordCheckerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CheckPassword(string password)
    {
        var response = await _httpClient.PostAsJsonAsync($"/CheckPassword?password={password}", password);

        if (response.IsSuccessStatusCode) return;

        var message = await response.Content.ReadAsStringAsync();
        throw new Exception(message);
    }
}