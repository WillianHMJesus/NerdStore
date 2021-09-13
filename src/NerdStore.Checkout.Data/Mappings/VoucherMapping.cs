using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Checkout.Domain.Entities;

namespace NerdStore.Checkout.Data.Mappings
{
    public class VoucherMapping : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(x => x.DiscountType)
                .HasConversion<int>();

            builder.ToTable("Vouchers");
        }
    }
}
