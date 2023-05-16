namespace Application.Passwords.Create;

public record GeneratePasswordEvent(Guid PasswordId)
{
    public int Length { get; set; }
    public string? Password { get; set; }
    public string? Rating { get; set; }
}

public record CheckGeneratedPassword(Guid PasswordId)
{
    public int Length { get; set; }
    public string? Password { get; set; }
    public string? Rating { get; set; }
}

public record GeneratedPasswordChecked(Guid PasswordId)
{
    public int Length { get; set; }
    public string? Password { get; set; }
    public string Rating { get; set; }
}