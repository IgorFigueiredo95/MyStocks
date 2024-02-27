using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStocks.Domain;
using MyStocks.Domain.PortfolioAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure.Persistence.Configurations;

public class PortfolioModelBuilder : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x=> x.Name)
            .HasMaxLength(Constants.MAX_PORTFOLIONAME_LENGTH)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(Constants.MAX_PORTFOLIDESCRIPTION_LENGTH)
            .IsRequired();
        
        builder.Property(x => x.SharesCount)
            .HasColumnType("integer");

        builder.HasIndex(x => x.Shares)
            .IsUnique();
        builder.Property(x => x.Shares)
            .HasColumnName("SharesId");

        builder.Property(x => x.CreatedAt)
            .HasConversion(
            destDate => DateTime.SpecifyKind(destDate, DateTimeKind.Utc),
            srcDate => DateTime.SpecifyKind(srcDate, DateTimeKind.Utc));

        builder.Property(x => x.CreatedAt)
            .HasConversion(
            destDate => DateTime.SpecifyKind(destDate, DateTimeKind.Utc),
            srcDate => DateTime.SpecifyKind(srcDate, DateTimeKind.Utc));

    }
}
