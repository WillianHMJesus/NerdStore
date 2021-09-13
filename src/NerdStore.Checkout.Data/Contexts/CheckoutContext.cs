using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NerdStore.Checkout.Domain.Entities;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Data;
using NerdStore.Core.Messages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Checkout.Data.Contexts
{
    public class CheckoutContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediator;

        public CheckoutContext(DbContextOptions<CheckoutContext> options,
            IMediatorHandler mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CheckoutContext).Assembly);

            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.HasSequence<int>("MySequency").StartsAt(1000).IncrementsBy(1);
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> CommitAsync()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("Register") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Register").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("Register").IsModified = false;
                }
            }

            var success = await base.SaveChangesAsync() > 0;
            if (success) await _mediator.PublishEventsAsync(this);

            return success;
        }
    }
}
