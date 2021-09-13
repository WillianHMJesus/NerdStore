using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Data;
using NerdStore.Core.Messages;
using NerdStore.Payment.Business.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using EntityPayment = NerdStore.Payment.Business.Entities.Payment;

namespace NerdStore.Payment.Data.Contexts
{
    public class PaymentContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediator;

        public PaymentContext(DbContextOptions<PaymentContext> options, 
            IMediatorHandler mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
        }

        public DbSet<EntityPayment> Payments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentContext).Assembly);

            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

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
