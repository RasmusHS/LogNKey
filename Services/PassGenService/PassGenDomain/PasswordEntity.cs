using System.Runtime.CompilerServices;
using PassGenDomain.PasswordDomainService;

namespace PassGenDomain;

//Det er her password genereringsprocessen ligger 
public class PasswordEntity
{
    internal PasswordEntity()
    {
    }

    public PasswordEntity(int length/*, string status*/)
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
        // Password generation algorithm goes here

        var options = new PasswordOptions
        {
            Length = this.Length,
            MinLowercase = 2,
            MinNumbers = 2,
            MinSpecial = 2,
            MinUppercase = 2,
            UseLowercase = true,
            UseNumbers = true,
            UseSpecial = true,
            UseUppercase = true,
        };
        var password = RandomPasswordGenerator.Generate(options);

        return password;
    }

    public void Edit(PasswordId passwordId, string rating)
    {

    }
}