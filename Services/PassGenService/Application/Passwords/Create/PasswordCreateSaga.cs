using Application.Data;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;

namespace Application.Passwords.Create;

// Orchestrator
public class PasswordCreateSaga : Saga<PasswordCreateSagaData>,
    IAmInitiatedBy<GeneratePasswordEvent>,
    IHandleMessages<GeneratedPasswordChecked>
{
    private readonly IBus _bus;
    private readonly IApplicationDbContext _context;

    public PasswordCreateSaga(IBus bus, IApplicationDbContext context)
    {
        _bus = bus;
        _context = context;
    }

    protected override void CorrelateMessages(ICorrelationConfig<PasswordCreateSagaData> config)
    {
        config
            .Correlate<GeneratePasswordEvent>(m => m.PasswordId, s => s.PasswordId);
        config
            .Correlate<GeneratedPasswordChecked>(m => m.PasswordId, s => s.PasswordId);
    }

    public async Task Handle(GeneratePasswordEvent message)
    {
        if (!IsNew) return;

        Data.PasswordGenerated = true;

        // Dette vil sende denne besked igennem køen og sætte gang i den respektive handler
        await _bus.Send(new CheckGeneratedPassword(message.PasswordId)
        {
            Length = message.Length, 
            Password = message.Password, 
            Rating = message.Rating
        }); // Starter næste step i saga
    }

    public Task Handle(GeneratedPasswordChecked message)
    {
        Data.GeneratedPasswordChecked = true;

        MarkAsComplete();

        return Task.CompletedTask;
    }
}