using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStocks.Domain;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Shares;
using MyStocks.Domain.SharesAggregate;
using MyStocks.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure.Persistence.Configurations
{
    internal class ShareDetailModelBuilder : IEntityTypeConfiguration<ShareDetail>
    {
        public void Configure(EntityTypeBuilder<ShareDetail> builder)
        {
            //Como geramos manualmente o Id guid, o contexto seta o state da entidade como "modified"
            //quando deveria ser "added" ao salvar no banco ocontexto espera achar essa entidade no banco e não acha, dando erro.
            //Com  "ValueGeneratedNever()", deixamos claro que o banco não gera esse Id.

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever();
            
            //dessa forma fica sem navigation property na child entity
            builder.HasOne<Share>()
                .WithMany(x => x.ShareDetails)
                .HasForeignKey(x=> x.ShareId)
                .IsRequired();

            builder.Property(x => x.Note)
                .HasMaxLength(Constants.MAX_SHAREDETAILNOTE_LENGTH);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.OwnsOne(x => x.Price);

            builder.Property(x => x.OperationType)
                .HasColumnName("OperationType")
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
