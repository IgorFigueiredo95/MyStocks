using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStocks.Domain;
using MyStocks.Domain.PortfolioAggregate;
using MyStocks.Domain.SharesAggregate;
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

        builder.Property(x => x.Name)
            .HasMaxLength(Constants.MAX_PORTFOLIONAME_LENGTH)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(Constants.MAX_PORTFOLIDESCRIPTION_LENGTH)
            .IsRequired();

        builder.Property(x => x.SharesCount)
            .HasColumnType("integer");

        builder.Property(x => x.CreatedAt)
            .HasConversion(
            destDate => DateTime.SpecifyKind(destDate, DateTimeKind.Utc),
            srcDate => DateTime.SpecifyKind(srcDate, DateTimeKind.Utc));

        builder.Property(x => x.CreatedAt)
            .HasConversion(
            destDate => DateTime.SpecifyKind(destDate, DateTimeKind.Utc),
            srcDate => DateTime.SpecifyKind(srcDate, DateTimeKind.Utc));

        builder.OwnsMany(x => x.ShareIds,
            navigationBuilder =>
            {
                navigationBuilder.HasKey(nb => new { nb.SharedId, nb.PortfolioId });
                navigationBuilder.Property(x => x.CreatedAt)
                    .HasConversion(
                        destDate => DateTime.SpecifyKind(destDate, DateTimeKind.Utc),
                        srcDate => DateTime.SpecifyKind(srcDate, DateTimeKind.Utc));
                navigationBuilder.Property(x => x.SharedId)
                    .ValueGeneratedNever();
                navigationBuilder.Property(x => x.PortfolioId)
                   .ValueGeneratedNever();
            });

    }
}
