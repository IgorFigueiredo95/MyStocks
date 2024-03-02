using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStocks.Domain;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Shares;
using MyStocks.Domain.SharesAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure.Persistence.Configurations
{
    public class ShareModelBuilder : IEntityTypeConfiguration<Share>
    {
        public void Configure(EntityTypeBuilder<Share> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.Code)
                .HasMaxLength(Constants.MAX_CODE_LENGTH)
                .IsRequired();

            builder.HasIndex(x => x.Code).IsUnique();

            builder.Property(x => x.Name)
                .HasMaxLength(Constants.MAX_SHARENAME_LENGTH)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(Constants.MAX_SHAREDESCRIPTION_LENGTH);

            builder.Property(x => x.ShareType)
                .IsRequired();

            builder.Property(x => x.TotalShares)
                .IsRequired();

            builder.OwnsOne(x => x.TotalValueInvested);

            builder.OwnsOne(x => x.AveragePrice);

            builder.HasMany(x => x.ShareDetails)
                .WithOne();

            builder.Property(x => x.CreatedAt)
                //Todo:Incluso para compatibilidade com formato de data EF https://duongnt.com/datetime-net6-postgresql/
                .HasConversion(
                srcDate => DateTime.SpecifyKind(srcDate, DateTimeKind.Utc),
                DstDate => DateTime.SpecifyKind(DstDate, DateTimeKind.Utc)
                );
            builder.Property(x => x.UpdatedAt)
                .HasConversion(
                srcDate => DateTime.SpecifyKind(srcDate, DateTimeKind.Utc),
                DstDate => DateTime.SpecifyKind(DstDate, DateTimeKind.Utc)
                );

        }
    }
}
