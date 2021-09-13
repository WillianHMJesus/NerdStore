using NerdStore.Core.Data;
using NerdStore.Payment.Business.Entities;
using NerdStore.Payment.Business.Interfaces;
using NerdStore.Payment.Data.Contexts;
using EntityPayment = NerdStore.Payment.Business.Entities.Payment;

namespace NerdStore.Payment.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentContext _context;

        public PaymentRepository(PaymentContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(EntityPayment payment)
        {
            _context.Payments.Add(payment);
        }

        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
