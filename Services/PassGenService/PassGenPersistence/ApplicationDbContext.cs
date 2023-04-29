using Microsoft.EntityFrameworkCore;
using PassGenApplication.Data;
using PassGenDomain;

namespace PassGenPersistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<PasswordEntity> Passwords { get; set; }
}