using NerdStore.Payment.Business.DTOs;
using NerdStore.Payment.Business.Entities;
using System.Threading.Tasks;

namespace NerdStore.Payment.Business.Interfaces
{
    public interface IPaymentService
    {
        Task<Transaction> MakePaymentOrder(PaymentOrderDTO paymentOrder);
    }
}
