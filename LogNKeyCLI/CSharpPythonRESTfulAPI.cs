using System.Net.Http.Json;

namespace LogNKeyCLI;

public class CSharpPythonRESTfulAPI : ICSharpPythonRESTfulAPI
{
    private readonly HttpClient _httpClient;

    public CSharpPythonRESTfulAPI(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// C# test to call Python HttpWeb RESTful API
    /// </summary>
    /// <param name="uirWebAPI">UIR web api link</param>
    /// <returns>Web response string</returns>
    //public async Task ApiTest(string uirWebAPI)
    //{
    //    string testInput = "123456";
    //    string webResponse = string.Empty;

    //    Uri uri = new Uri(uirWebAPI);
    //    HttpClient httpWebRequest = new HttpClient();
    //    httpWebRequest.BaseAddress = uri;

    //    var response = await httpWebRequest.PostAsJsonAsync(uri, value: testInput);

    //    if (response.IsSuccessStatusCode) return;

    //    var message = await response.Content.ReadAsStringAsync();
    //    throw new Exception(message);
    //}
    async Task<PasswordCheck> ICSharpPythonRESTfulAPI.ApiTest(string uirWebAPI)
    {
        //string testInput = "123456";

        return await _httpClient.GetFromJsonAsync<PasswordCheck>(requestUri: uirWebAPI);

        //if (response.IsSuccessStatusCode) return;

        //var message = await response.Content.ReadAsStringAsync();
        //throw new Exception(message);
    }
}