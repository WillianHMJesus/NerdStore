using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Core.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Data.Contexts
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => 
                e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                //property.Relational().ColumnType = "varchar(100)";
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }

        public async Task<bool> CommitAsync()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Register") != null))
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Property("Register").CurrentValue = DateTime.Now;
                }

                if(entry.State == EntityState.Modified)
                {
                    entry.Property("Register").IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }
    }
}
