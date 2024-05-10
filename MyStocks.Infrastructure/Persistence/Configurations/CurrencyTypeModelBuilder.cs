using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStocks.Domain.Currencies;
using MyStocks.Domain;
using MyStocks.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyStocks.Domain.Users;

namespace MyStocks.Application.Currencies
{
    public class CurrencyTypeModelBuilder : IEntityTypeConfiguration<CurrencyTypes>
    {

        public void Configure(EntityTypeBuilder<CurrencyTypes> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).IsRequired();

            builder.Property(c => c.Code).HasMaxLength(Constants.MAX_CODE_LENGTH);
            builder.HasIndex(c => c.Code).IsUnique();

            builder.Property(c => c.CurrencyCode).HasMaxLength(Constants.MAX_CURRENCYCODE_LENGTH);
            builder.HasIndex(c => c.CurrencyCode).IsUnique();

            builder.Property(c => c.Name).HasMaxLength(Constants.MAX_CURRENCYNAME_LENGTH);

            builder.Property(c => c.IsDefault).IsRequired();

            builder.Property(c => c.OwnerId).IsRequired();

            builder.HasOne<User>()
                .WithMany().
                HasForeignKey(c => c.OwnerId);

            //var Identity = Thread.CurrentPrincipal.Identity.Name;
            //builder.HasQueryFilter(x => x.OwnerId == Guid.Parse(Identity));
        }
    }
}
