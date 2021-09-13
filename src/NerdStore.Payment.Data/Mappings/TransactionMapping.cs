using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Payment.Business.Entities;

namespace NerdStore.Payment.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TransactionStatus)
                .HasConversion<int>();

            builder.HasOne(x => x.Payment)
                .WithOne(x => x.Transaction)
                .HasForeignKey<Transaction>(x => x.PaymentId);
        }
    }
}
