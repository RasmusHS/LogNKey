using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;

namespace Application.Passwords.Create;

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
        if else checks, der checker om der Password strengen er tom eller ej.
        Hvis den ikke er tom, så sendes Password strengen til ML modellen.
        Hvis Password bliver bedømmet til at være "Weak", så sender den fejl besked tilbage til brugeren uden at gemme i Password db
        */
        await Task.Delay(2000); // Dette er til for simulere at et password bliver checked. 
        //if (IsNullOrEmpty(message.Password))
        //_logger.LogInformation("ERROR: No Password received {@PasswordId}", message.PasswordId);

        //var source = engine.CreateScriptSourceFromFile(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Services/PassCheckerService/PassChecker.py"));

        var psi = new ProcessStartInfo();
        //psi.FileName = @"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python39_64\python.exe";
        psi.FileName = @"C:\tools\Anaconda3\envs\passMLEnv\python.exe";

        var script = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Services\\PassCheckerService\\PassChecker.py");

        var testPass = "123456";

        psi.Arguments = $"\"{script}\" \"{testPass}\"";

        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        var errors = "";
        var results = "";

        var process = new Process();
        process.StartInfo = psi;

        process.Start();

        var streamReader = process.StandardOutput;

        var checkedPassword = streamReader.ReadLineAsync();

        await process.WaitForExitAsync();
        process.Close();


        //using (var process = Process.Start(psi))
        //{
        //    errors = process.StandardError.ReadToEnd();
        //    results = process.StandardOutput.ReadToEnd();
        //}

        Console.WriteLine("ERRORS:");
        Console.WriteLine(errors);
        Console.WriteLine();
        Console.WriteLine("Results:");
        Console.WriteLine(checkedPassword);

        _logger.LogInformation("Password checked {@PasswordId}, {@results}", message.PasswordId, checkedPassword);

        await _bus.Send(new GeneratedPasswordChecked(message.PasswordId) {Password = message.Password}); //Saga slut
    }
}