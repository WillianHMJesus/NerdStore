using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EntityPayment = NerdStore.Payment.Business.Entities.Payment;

namespace NerdStore.Payment.Data.Mappings
{
    public class PaymentMapping : IEntityTypeConfiguration<EntityPayment>
    {
        public void Configure(EntityTypeBuilder<EntityPayment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(x => x.CardName)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.CardNumber)
                .IsRequired()
                .HasColumnType("varchar(16)");

            builder.Property(x => x.CardExpiration)
                .IsRequired()
                .HasColumnType("varchar(5)");

            builder.Property(x => x.CardSecurityCode)
                .IsRequired()
                .HasColumnType("varchar(4)");
        }
    }
}
