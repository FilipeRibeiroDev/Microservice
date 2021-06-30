using Account.Domain.AggregatesModel.UserAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Infrastructure.EntityConfigurations
{
    class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user", AccountContext.DEFAULT_SCHEMA);

            builder.Property(b => b.Id)
                .UseHiLo("userseq", AccountContext.DEFAULT_SCHEMA);

            builder.Property(b => b.IdentityUser)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex("IdentityUser")
              .IsUnique(true);

            builder.Property(b => b.Name);

            builder.Property(b => b.Email);

            builder.Property(b => b.Password);

            builder.Property(b => b.CreatedDate);
        }
    }
}
