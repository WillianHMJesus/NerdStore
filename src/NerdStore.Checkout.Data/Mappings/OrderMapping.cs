using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Checkout.Domain.Entities;
using System;

namespace NerdStore.Checkout.Data.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Code)
                .HasDefaultValueSql("NEXT VALUE FOR MySequency");

            builder.Property(x => x.OrderStatus)
                .HasConversion<int>();

            builder.Property<Guid>("VoucherId");
            builder.HasOne(x => x.Voucher)
                .WithOne(x => x.Order)
                .HasForeignKey<Order>("VoucherId");

            builder.ToTable("Orders");
        }
    }
}
