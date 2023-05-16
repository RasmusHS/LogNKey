namespace LogNKeyCLI;

public interface ICSharpPythonRESTfulAPI
{
    Task<PasswordCheck> ApiTest(string uirWebAPI);
}