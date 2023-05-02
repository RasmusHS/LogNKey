using Carter;
using MediatR;
using Application.Passwords.Create;
using Mapster;

namespace LogNKey.API.Endpoints.Passwords;

public class Create : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("passwords", async (CreatePasswordRequest request, ISender sender) =>
        {
            //var command = new CreatePasswordCommand();

            CreatePasswordCommand command2 = request.Adapt<CreatePasswordCommand>();

            await sender.Send(command2);

            return Results.Ok();
        });
    }
}

public sealed record CreatePasswordRequest(
    int Length);