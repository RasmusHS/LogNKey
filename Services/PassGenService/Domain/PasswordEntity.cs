using System.Runtime.CompilerServices;
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
        Id = new PasswordId(Guid.NewGuid());

        Password = CreatePassword();
    }

    public PasswordId Id { get; private set; }
    public string? Password { get; private set; }
    public int Length { get; private set; }
    public string? Rating { get; private set; }

    public string CreatePassword()
    {
        var pwd = new Password(Length);

        var password = pwd.Next();

        return password;
    }

    public void Edit(PasswordId passwordId, string rating)
    {

    }
}