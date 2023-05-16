using MediatR;

namespace Application.Passwords.Create;

public record CreatePasswordCommand() : IRequest
{
    public int Length { get; init; }
    public string? Password { get; set; }
    public string? Rating { get; set; }
}