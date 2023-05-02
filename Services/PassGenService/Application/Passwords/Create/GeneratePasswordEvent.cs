namespace Application.Passwords.Create;

public record GeneratePasswordEvent(Guid PasswordId);

public record CheckGeneratedPassword(Guid PasswordId);

public record GeneratedPasswordChecked(Guid PasswordId);