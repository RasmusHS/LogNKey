using Rebus.Sagas;

namespace Application.Passwords.Create;

public class PasswordCreateSagaData : ISagaData
{
    public Guid Id { get; set; }
    public int Revision { get; set; }

    public Guid PasswordId { get; set; }
    public bool PasswordGenerated { get; set; }
    public bool GeneratedPasswordChecked { get; set; }
}