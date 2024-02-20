using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStocks.Domain;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Shares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure.Persistence.Configurations
{
    internal class ShareDetailModelBuilder : IEntityTypeConfiguration<ShareDetail>
    {
        public void Configure(EntityTypeBuilder<ShareDetail> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Share)
                .WithMany()
                .HasForeignKey(x => x.ShareId)
                .IsRequired();

            builder.Property(x => x.Note)
                .HasMaxLength(Constants.MAX_SHAREDETAILNOTE_LENGTH);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.OwnsOne(x => x.Price);

            builder.Property(x => x.OperandType)
                .IsRequired();

            builder.Property(x => x.CreatedAt).HasConversion(
                srcdate => DateTime.SpecifyKind(srcdate, DateTimeKind.Utc),
                destDate => DateTime.SpecifyKind(destDate, DateTimeKind.Utc));

            builder.Property(x => x.UpdatedAt).HasConversion(
                srcdate => DateTime.SpecifyKind(srcdate, DateTimeKind.Utc),
                destDate => DateTime.SpecifyKind(destDate, DateTimeKind.Utc));

        }
    }
}
