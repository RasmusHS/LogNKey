using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PassGenDomain;

namespace PassGenApplication.Data;

public interface IApplicationDbContext
{
    DbSet<PasswordEntity> Passwords { get; set; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}