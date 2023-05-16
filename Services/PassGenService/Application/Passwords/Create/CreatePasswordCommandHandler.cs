using MediatR;
using Application.Data;
using Domain;
using Rebus.Bus;

namespace Application.Passwords.Create;

public sealed class CreatePasswordCommandHandler : IRequestHandler<CreatePasswordCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IBus _bus;

    public CreatePasswordCommandHandler(IApplicationDbContext context, IBus bus)
    {
        _context = context;
        _bus = bus;
    }

    public async Task Handle(CreatePasswordCommand request, CancellationToken cancellationToken)
    {
        var password = new PasswordEntity(request.Length);

        _context.Passwords.Add(password);

        await _context.SaveChangesAsync(cancellationToken);

        // Implement Outbox pattern here?
        // If RabbitMQ goes down then the password can't be sent through the bus
        await _bus.Send(new GeneratePasswordEvent(password.Id.Value)
        {
            Length = password.Length, 
            Password = password.Password, 
            Rating = password.Rating
        });
    }
}