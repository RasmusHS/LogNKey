using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;

namespace PassGenApplication.Passwords.Create;

internal sealed class CheckGeneratedPasswordHandler : IHandleMessages<CheckGeneratedPassword>
{
    private readonly ILogger<CheckGeneratedPasswordHandler> _logger;
    private readonly IBus _bus;

    public CheckGeneratedPasswordHandler(ILogger<CheckGeneratedPasswordHandler> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public async Task Handle(CheckGeneratedPassword message)
    {
        _logger.LogInformation("Sending password {@PasswordId} to be checked", message.PasswordId);

        /*
        TODO: 
        Udskift med logik der sender password til ML modellen for at blive checket for styrke og hvis styrken er "Weak",
        så fejler sagaen og starter forfra af sig selv indtil en rating af "Medium" eller højere er opnået.
        */
        await Task.Delay(2000); // Dette er til for simulere at et password bliver checked. 

        _logger.LogInformation("Password checked {@PasswordId}", message.PasswordId);

        await _bus.Send(new GeneratedPasswordChecked(message.PasswordId));
    }
}