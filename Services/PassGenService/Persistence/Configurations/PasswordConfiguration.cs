using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Persistence.Configurations;

public class PasswordConfiguration : IEntityTypeConfiguration<PasswordEntity>
{
    public void Configure(EntityTypeBuilder<PasswordEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasConversion(
            password => password.Value,
            value => new PasswordId(value));
    }
}