using Carter;
using MediatR;
using PassGenApplication.Passwords.Create;

namespace LogNKey.API.Endpoints.Passwords;

public class Create : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("passwords", async (ISender sender) =>
        {
            var command = new CreatePasswordCommand();

            await sender.Send(command);

            return Results.Ok();
        });
    }
}