using NerdStore.Core.Data;
using NerdStore.Payment.Business.Entities;
using PaymentEntity = NerdStore.Payment.Business.Entities.Payment;

namespace NerdStore.Payment.Business.Interfaces
{
    public interface IPaymentRepository : IRepository<PaymentEntity>
    {
        void Add(PaymentEntity payment);
        void AddTransaction(Transaction transaction);
    }
}
