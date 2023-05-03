namespace Application.Passwords.Create;

public record GeneratePasswordEvent(Guid PasswordId)
{
    public string? Password { get; set; }
}

public record CheckGeneratedPassword(Guid PasswordId)
{
    public string? Password { get; set; }
}

public record GeneratedPasswordChecked(Guid PasswordId)
{
    public string? Password { get; set; }
    public string ResponseMessage { get; set; }
}