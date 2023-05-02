﻿using MediatR;
using Application.Data;
using Domain;
using Rebus.Bus;

namespace Application.Passwords.Create;

internal sealed class CreatePasswordCommandHandler : IRequestHandler<CreatePasswordCommand>
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

        await _bus.Send(new GeneratePasswordEvent(password.Id.Value));
    }
}