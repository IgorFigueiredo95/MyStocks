using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStocks.Domain.Users;
using MyStocks.Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure.Persistence.Configurations;

public class UserModelBuilder : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x=> x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.FirstName)
            .HasMaxLength(UserConstants.MAX_USERNAME_LENGTH)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(UserConstants.MAX_USERLASTNAME_LENGTH)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasConversion(dest => dest.Address, src => Email.Create(src))
            .IsRequired();

        builder.Property(x => x.Password)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasConversion(
                dest => DateTime.SpecifyKind(dest, DateTimeKind.Utc),
                src => DateTime.SpecifyKind(src, DateTimeKind.Utc));

        builder.Property(x => x.UpdatedAt)
            .HasConversion(
                dest => DateTime.SpecifyKind(dest, DateTimeKind.Utc),
                src => DateTime.SpecifyKind(src, DateTimeKind.Utc));

    }
}
