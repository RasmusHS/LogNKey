using System.Diagnostics;
using System.Net.Http.Json;
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
    //private readonly PasswordCheckerService _passwordCheckerService;
    private readonly HttpClient _httpClient;
    private readonly IApplicationDbContext _context;

    public CheckGeneratedPasswordHandler(ILogger<CheckGeneratedPasswordHandler> logger, IBus bus, HttpClient httpClient, IPasswordCheckService passwordCheckService, IApplicationDbContext context)
    {
        _logger = logger;
        _bus = bus;
        //_passwordCheckerService = passwordCheckerService;
        _httpClient = httpClient;
        _passwordCheckService = passwordCheckService;
        _context = context;
    }

    public async Task Handle(CheckGeneratedPassword message)
    {
        _logger.LogInformation("Sending password {@PasswordId} to be checked", message.PasswordId);

        /*
        TODO: 
        Udskift med logik der sender password til ML modellen for at blive checket for styrke og hvis styrken er "Weak",
        så fejler sagaen og starter forfra af sig selv indtil en rating af "Medium" eller højere er opnået.
        if else checks, der checker om der Password strengen er tom eller ej.
        Hvis den ikke er tom, så sendes Password strengen til ML modellen.
        Hvis Password bliver bedømmet til at være "Weak", så sender den fejl besked tilbage til brugeren uden at gemme i Password db
        */
        //await Task.Delay(2000); // Dette er til for simulere at et password bliver checked. 
        //if (IsNullOrEmpty(message.Password))
        //_logger.LogInformation("ERROR: No Password received {@PasswordId}", message.PasswordId);

        //await CheckPassword(message.Password);
        //https://www.google.com/search?client=firefox-b-d&q=python+fastapi+take+requests+from+c%23+httpClient
        //https://ernest-bonat.medium.com/using-c-to-call-python-restful-api-web-services-with-machine-learning-models-6d1af4b7787e

        string input = message.Password;

        string uirWebAPI = $"http://host.docker.internal:8008/CheckPassword/{input}";

        GeneratedPasswordChecked checkPassword = await _passwordCheckService.ApiTest(uirWebAPI);

        if (checkPassword.Rating != "Weak")
        {
            var updateTheMediumAndTheStrong = (from passwords in _context.Passwords
                where passwords.Id.Value == message.PasswordId
                select passwords).FirstOrDefault();

            updateTheMediumAndTheStrong.Edit(checkPassword.Rating);

            _context.Passwords.Update(updateTheMediumAndTheStrong);
        }
        else
        {
            var deleteTheWeak = (from passwords in _context.Passwords
                where passwords.Id.Value == message.PasswordId
                select passwords).FirstOrDefault();
            _context.Passwords.Remove(deleteTheWeak);
        }

        _logger.LogInformation("Password checked {@PasswordId}", message.PasswordId);

        await _bus.Send(new GeneratedPasswordChecked(message.PasswordId)
        {
            Length = message.Length,
            Password = message.Password, 
            Rating = checkPassword.Rating
        }); //Saga slut
    }
}