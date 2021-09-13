using NerdStore.Payment.Business.DTOs;
using NerdStore.Payment.Business.Entities;
using PaymentEntity = NerdStore.Payment.Business.Entities.Payment;

namespace NerdStore.Payment.Business.Interfaces
{
    public interface ICreditCardPaymentFacade
    {
        Transaction MakePayment(OrderDTO order, PaymentEntity payment);
    }
}
