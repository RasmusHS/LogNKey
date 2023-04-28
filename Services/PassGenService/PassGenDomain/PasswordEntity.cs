namespace PassGenDomain;

//Det er her password genereringsprocessen ligger 
public class PasswordEntity
{
    internal PasswordEntity()
    {
    }

    public PasswordEntity(int length, string status)
    {
        Length = length;
        Status = status;

        Password = CreatePassword(Length);
    }


    public string? Password { get; }
    public int Length { get; }
    public string? Rating { get; private set; }
    public string Status { get; }

    public string CreatePassword(int length)
    {
        return new NotImplementedException().ToString();
    }
}