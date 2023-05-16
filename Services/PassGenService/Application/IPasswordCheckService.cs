using Application.Passwords.Create;

namespace Application;

public interface IPasswordCheckService
{
    Task<GeneratedPasswordChecked> ApiTest(string uirWebAPI);
}