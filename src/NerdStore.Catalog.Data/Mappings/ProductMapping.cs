using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalog.Domain.Entities;
using System;

namespace NerdStore.Catalog.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(x => x.Image)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.OwnsOne(x => x.Dimension, d =>
            {
                d.Property(x => x.Height)
                    .HasColumnName("Height")
                    .HasColumnType("decimal(5, 2)");

                d.Property(x => x.Width)
                    .HasColumnName("Width")
                    .HasColumnType("decimal(5, 2)");

                d.Property(x => x.Depth)
                    .HasColumnName("Depth")
                    .HasColumnType("decimal(5, 2)");
            });

            builder.Property<Guid>("CategotyId");
            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey("CategotyId");

            builder.ToTable("Products");
        }
    }
}
