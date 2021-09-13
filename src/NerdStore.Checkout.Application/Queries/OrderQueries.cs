using NerdStore.Checkout.Application.Queries.DTOs;
using NerdStore.Checkout.Domain.Entities.Enums;
using NerdStore.Checkout.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Checkout.Application.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueries(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CartDTO> GetCartByClientAsync(Guid clientId)
        {
            var order = await _orderRepository.GetOrderDraftByClientAsync(clientId);
            if (order == null) return null;

            var cart = new CartDTO
            {
                OrderId = order.Id,
                ClientId = order.ClientId,
                Amount = order.Amount,
                DiscountValue = order.Discount,
                SubTotal = order.Amount - order.Discount
            };

            if (order.Voucher != null)
            {
                cart.VoucherCode = order.Voucher.Code;
            }

            foreach (var item in order.OrderItems)
            {
                cart.Items.Add(new CartItemDTO
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitValue = item.UnitValue,
                    Amount = item.UnitValue * item.Quantity
                });
            }

            return cart;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrderByClient(Guid clientId)
        {
            var orders = await _orderRepository.GetByClientAsync(clientId);
            orders = orders.Where(x => x.OrderStatus == OrderStatus.Paid || 
                x.OrderStatus == OrderStatus.Cancelled ||
                x.OrderStatus == OrderStatus.Finished)
                .OrderByDescending(x => x.Code);

            if (orders.Any()) return null;

            var ordersDTO = new List<OrderDTO>();
            foreach (var order in orders)
            {
                ordersDTO.Add(new OrderDTO
                {
                    Amount = order.Amount,
                    OrderStatus = (int)order.OrderStatus,
                    Code = order.Code,
                    Register = order.Register
                });
            }

            return ordersDTO;
        }
    }
}
