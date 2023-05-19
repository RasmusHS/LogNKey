using Application.Passwords.Create;
using System.Net.Http.Json;

namespace Application;

public class PasswordCheckService : IPasswordCheckService
{
    private readonly HttpClient _httpClient;

    public PasswordCheckService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    async Task<GeneratedPasswordChecked> IPasswordCheckService.GetPasswordRating(string uirWebAPI)
    {
        return await _httpClient.GetFromJsonAsync<GeneratedPasswordChecked>(requestUri: uirWebAPI);
    }
}