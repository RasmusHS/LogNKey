using LogNKeyCLI;

class Program
{
    public static async Task Main(string[] args)
    {
        string uirWebAPI;
        string testInput = "123456";
        string exceptionMessage;
        string webResponse;

        uirWebAPI = $"http://localhost:8008/CheckPassword/{testInput}";
        exceptionMessage = string.Empty;

        //CSharpPythonRESTfulAPI csharpPythonRESTfulAPI = new CSharpPythonRESTfulAPI();
        HttpClient _httpClient = new HttpClient();
        ICSharpPythonRESTfulAPI csharpPythonRESTfulAPI = new CSharpPythonRESTfulAPI(_httpClient);
        PasswordCheck passwordCheck = new PasswordCheck();

        passwordCheck = await csharpPythonRESTfulAPI.ApiTest(uirWebAPI);

        Console.WriteLine(passwordCheck.Password);
        Console.WriteLine(passwordCheck.Rating);

        //webResponse = await csharpPythonRESTfulAPI.ApiTest(uirWebAPI);
        //webResponse = await _restClient.ApiTest(uirWebAPI);
    }
}