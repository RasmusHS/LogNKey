namespace PassGenDomain.PasswordDomainService;

public class PasswordOptions
{
    public bool UseLowercase { get; set; }

    public bool UseUppercase { get; set; }

    public bool UseNumbers { get; set; }

    public bool UseSpecial { get; set; }

    public int MinLowercase { get; set; }

    public int MinUppercase { get; set; }

    public int MinNumbers { get; set; }

    public int MinSpecial { get; set; }

    public int Length { get; set; }
}