using MediatR;

namespace PassGenApplication.Passwords.Create;

public record CreatePasswordCommand() : IRequest
{
    public int Length { get; init; }
}