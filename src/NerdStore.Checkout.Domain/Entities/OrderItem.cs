using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Checkout.Domain.Entities
{
    public class OrderItem : Entity
    {
        public OrderItem(Guid productId, string productName, int quantity, decimal unitValue)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitValue = unitValue;
        }

        protected OrderItem() { }

        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitValue { get; private set; }
        public decimal Amount => UnitValue * Quantity;
        public Order Order { get; private set; }

        public override bool IsValid()
        {
            return true;
        }

        internal void AssignOrder(Guid orderId)
        {
            OrderId = orderId;
        }

        internal void AddUnit(int unit)
        {
            Quantity += unit;
        }

        internal void UpdateUnit(int unit)
        {
            Quantity = unit;
        }
    }
}
