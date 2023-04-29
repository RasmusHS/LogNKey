using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;

namespace PassGenApplication.Passwords.Create;

// Orchestrates the individual steps of the Saga
public class PasswordCreateSaga : Saga<PasswordCreateSagaData>,
    IAmInitiatedBy<GeneratePasswordEvent>,
    IHandleMessages<GeneratedPasswordChecked>
{
    private readonly IBus _bus;

    public PasswordCreateSaga(IBus bus)
    {
        _bus = bus;
    }

    protected override void CorrelateMessages(ICorrelationConfig<PasswordCreateSagaData> config)
    {
        config
            .Correlate<GeneratePasswordEvent>(m => m.PasswordId, s => s.PassGenId);
        config
            .Correlate<GeneratedPasswordChecked>(m => m.PasswordId, s => s.PassGenId);
    }

    public async Task Handle(GeneratePasswordEvent message)
    {
        if (!IsNew) return;

        Data.PasswordGenerated = true;

        // Dette vil sende denne besked igennem køen og sætte gang i den respektive handler
        await _bus.Send(new CheckGeneratedPassword(message.PasswordId)); // Starter næste step i saga
    }

    public Task Handle(GeneratedPasswordChecked message)
    {
        Data.GeneratedPasswordChecked = true;

        MarkAsComplete();

        return Task.CompletedTask;
    }
}