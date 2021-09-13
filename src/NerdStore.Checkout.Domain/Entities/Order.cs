using NerdStore.Checkout.Domain.Entities.Enums;
using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Checkout.Domain.Entities
{
    public class Order : Entity, IAggregateRoot
    {
        public Order(Guid clientId)
        {
            ClientId = clientId;
            OrderStatus = OrderStatus.Draft;
            Register = DateTime.Now;
            _orderItems = new List<OrderItem>();
        }

        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public int Code { get; private set; }
        public Guid ClientId { get; private set; }
        public bool VoucherUsed { get; private set; }
        public decimal Discount { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Register { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public Voucher Voucher { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public void ApplyVoucher(Voucher voucher)
        {
            Voucher = voucher;
            VoucherUsed = true;
            CalculateOrderValue();
        }

        public void CalculateOrderValue()
        {
            Amount = OrderItems.Sum(x => x.Amount);
            CalculateOrderValueWithDiscount();
        }

        public void CalculateOrderValueWithDiscount()
        {
            if (!VoucherUsed) return;

            decimal discount;
            var value = Amount;
            
            if (Voucher.DiscountType == DiscountType.Percent
                && Voucher.Percentage.HasValue)
            {
                discount = (value * Voucher.Percentage.Value) / 100;
            }
            else
            {
                discount = Voucher.DiscountValue.HasValue ?
                Voucher.DiscountValue.Value : 0;
            }
            
            value -= discount;
            Amount = value < 0 ? 0 : value;
            Discount = discount;
        }

        public bool OrderItemExisting(OrderItem orderItem)
        {
            return _orderItems.Any(x => x.ProductId == orderItem.ProductId);
        }

        public void AddItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            item.AssignOrder(Id);
            if(OrderItemExisting(item))
            {
                var itemExisting = _orderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                itemExisting.AddUnit(item.Quantity);
                item = itemExisting;

                _orderItems.Remove(itemExisting);
            }

            _orderItems.Add(item);
            CalculateOrderValue();
        }

        public void RemoveItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            var itemExisting = _orderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
            AssertionConcern.ValidateNull(itemExisting, "O item não pertence ao pedido");
            
            _orderItems.Remove(itemExisting);
            CalculateOrderValue();
        }

        public void UpdateItem(OrderItem item)
        {
            if (!item.IsValid()) return;
            
            item.AssignOrder(Id);
            var itemExisting = _orderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
            AssertionConcern.ValidateNull(itemExisting, "O item não pertence ao pedido");
            
            _orderItems.Remove(itemExisting);
            _orderItems.Add(item);
            CalculateOrderValue();
        }

        public void UpdateUnitItem(OrderItem item, int unit)
        {
            item.UpdateUnit(unit);
            UpdateItem(item);
        }

        public void StartOrder()
        {
            OrderStatus = OrderStatus.Started;
        }

        public void FinalizeOrder()
        {
            OrderStatus = OrderStatus.Paid;
        }

        public void CancelOrder()
        {
            OrderStatus = OrderStatus.Cancelled;
        }
    }
}
