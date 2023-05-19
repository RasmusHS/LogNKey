using Application.Data;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;

namespace Application.Passwords.Create;

public sealed class CheckGeneratedPasswordHandler : IHandleMessages<CheckGeneratedPassword>
{
    private readonly ILogger<CheckGeneratedPasswordHandler> _logger;
    private readonly IBus _bus;
    private readonly IPasswordCheckService _passwordCheckService;
    private readonly IApplicationDbContext _context;

    public CheckGeneratedPasswordHandler(ILogger<CheckGeneratedPasswordHandler> logger, IBus bus, IPasswordCheckService passwordCheckService, IApplicationDbContext context)
    {
        _logger = logger;
        _bus = bus;
        _passwordCheckService = passwordCheckService;
        _context = context;
    }

    public async Task Handle(CheckGeneratedPassword message)
    {
        _logger.LogInformation("Sending password {@PasswordId} to be checked", message.PasswordId);
        CancellationToken cancellationToken = default;

        string input = message.Password;

        string uirWebAPI = $"http://host.docker.internal:8008/CheckPassword/{input}";

        GeneratedPasswordChecked checkPassword = await _passwordCheckService.GetPasswordRating(uirWebAPI);

        if (checkPassword.Rating != "Weak")
        {
            var updateTheMediumAndTheStrong = _context.Passwords.FindAsync(message.PasswordId).Result;

            updateTheMediumAndTheStrong.Edit(checkPassword.Rating);

            _context.Passwords.Update(updateTheMediumAndTheStrong);
            await _context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            var deleteTheWeak = _context.Passwords.FindAsync(message.PasswordId).Result;
            _context.Passwords.Remove(deleteTheWeak);
            await _context.SaveChangesAsync(cancellationToken);
        }

        _logger.LogInformation("Password checked {@PasswordId}", message.PasswordId);

        await _bus.Send(new GeneratedPasswordChecked(message.PasswordId)
        {
            Length = message.Length,
            Password = message.Password, 
            Rating = checkPassword.Rating
        }); 
    }
}