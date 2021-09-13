using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Checkout.Domain.Entities;

namespace NerdStore.Checkout.Data.Mappings
{
    public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductName)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Ignore(x => x.Amount);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderId);

            builder.ToTable("OrderItems");
        }
    }
}
