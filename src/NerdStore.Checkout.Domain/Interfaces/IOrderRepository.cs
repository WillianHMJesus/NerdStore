using NerdStore.Checkout.Domain.Entities;
using NerdStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Checkout.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        #region Order
        void Add(Order order);
        Task<IEnumerable<Order>> GetByClientAsync(Guid clientId);
        Task<Order> GetByIdAsync(Guid id);
        Task<Order> GetOrderDraftByClientAsync(Guid clientId);
        void Update(Order order);
        #endregion

        #region OrderItem
        void AddItem(OrderItem item);
        Task<OrderItem> GetItemByIdAsync(Guid id);
        Task<OrderItem> GetItemByOrderAsync(Guid orderId, Guid productId);
        void RemoveItem(OrderItem item);
        void UpdateItem(OrderItem item);
        #endregion

        #region Voucher
        Task<Voucher> GetVoucherByCodeAsync(string code);
        #endregion
    }
}
