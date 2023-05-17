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
            CreatePasswordCommand command = request.Adapt<CreatePasswordCommand>();

            await sender.Send(command);

            return Results.Ok();
        });
    }
}

public sealed record CreatePasswordRequest
(
    int Length,
    string? Password,
    string? Rating
);
    