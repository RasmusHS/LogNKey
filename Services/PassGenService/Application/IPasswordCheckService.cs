using Application.Passwords.Create;

namespace Application;

public interface IPasswordCheckService
{
    Task<GeneratedPasswordChecked> GetPasswordRating(string uirWebAPI);
}