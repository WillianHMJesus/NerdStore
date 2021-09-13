using Microsoft.EntityFrameworkCore;
using NerdStore.Checkout.Data.Contexts;
using NerdStore.Checkout.Domain.Entities;
using NerdStore.Checkout.Domain.Entities.Enums;
using NerdStore.Checkout.Domain.Interfaces;
using NerdStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Checkout.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CheckoutContext _context;

        public OrderRepository(CheckoutContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        #region Order
        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public async Task<IEnumerable<Order>> GetByClientAsync(Guid clientId)
        {
            return await _context.Orders.AsNoTracking().Where(x => x.ClientId == clientId).ToListAsync();
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Order> GetOrderDraftByClientAsync(Guid clientId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => 
                x.ClientId == clientId && x.OrderStatus == OrderStatus.Draft);

            if (order == null) return null;

            await _context.Entry(order)
                .Collection(x => x.OrderItems).LoadAsync();

            if(order.Voucher != null)
            {
                await _context.Entry(order)
                    .Reference(x => x.Voucher).LoadAsync();
            }

            return order;
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }
        #endregion

        #region OrderItem
        public void AddItem(OrderItem item)
        {
            _context.OrderItems.Add(item);
        }

        public async Task<OrderItem> GetItemByIdAsync(Guid id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> GetItemByOrderAsync(Guid orderId, Guid productId)
        {
            return await _context.OrderItems.AsNoTracking()
                .FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
        }

        public void RemoveItem(OrderItem item)
        {
            _context.OrderItems.Remove(item);
        }

        public void UpdateItem(OrderItem item)
        {
            _context.OrderItems.Update(item);
        }
        #endregion

        #region Voucher
        public async Task<Voucher> GetVoucherByCodeAsync(string code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(x => x.Code == code);
        }
        #endregion

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
