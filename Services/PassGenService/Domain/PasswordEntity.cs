using PasswordGenerator;

namespace Domain;

public class PasswordEntity
{
    internal PasswordEntity()
    {
    }

    public PasswordEntity(int length)
    {
        Length = length;
        Id = Guid.NewGuid();
        Rating = "PENDING";

        Password = CreatePassword();
    }

    public Guid Id { get; private set; }
    public string? Password { get; private set; }
    public int Length { get; private set; }
    public string? Rating { get; private set; }

    public string CreatePassword()
    {
        var pwd = new Password(Length).IncludeLowercase().IncludeUppercase().IncludeNumeric().IncludeSpecial("!#£¤$%&{([)]=}?+|^¨~*,._-½§");

        var password = pwd.Next();

        return password;
    }

    public void Edit(string rating)
    {
        Rating = rating;
    }
}