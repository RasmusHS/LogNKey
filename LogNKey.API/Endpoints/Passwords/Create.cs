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

            CreatePasswordCommand command = request.Adapt<CreatePasswordCommand>();

            await sender.Send(command);

            return Results.Ok();
        });
        //app.MapPost("passwords", async (CreatePasswordRequest request, IBus bus) =>
        //{
        //    //var command = new CreatePasswordCommand();

        //    CreatePasswordCommand command = request.Adapt<CreatePasswordCommand>();

        //    await bus.Send(command);

        //    return Results.Ok();
        //});
    }
}

public sealed record CreatePasswordRequest
(
    int Length,
    string? Password,
    string? Rating
);
    