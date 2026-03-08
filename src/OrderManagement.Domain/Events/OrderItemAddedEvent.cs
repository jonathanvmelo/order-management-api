using OrderManagement.Domain.Common;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Events
{
    public class OrderItemAddedEvent : DomainEvent
    {
        public Guid OrderId { get; }
        public Guid OrderItemId { get; }
        public Guid ProductId { get; }
        public Quantity Quantity { get; }
        public Money UnitPrice { get; }

        public OrderItemAddedEvent(
            Guid orderId,
            Guid orderItemId,
            Guid productId,
            Quantity quantity,
            Money unitPrice)
        {
            OrderId = orderId;
            OrderItemId = orderItemId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

    }
}