using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PassGenDomain;

namespace PassGenPersistence.Configurations
{
    internal class PasswordConfiguration : IEntityTypeConfiguration<PasswordEntity>
    {
        public void Configure(EntityTypeBuilder<PasswordEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasConversion(
                password => password.Value,
                value => new PasswordId(value));
        }
    }
}
