using OrderManagement.Domain.Common;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Events
{
    public class OrderItemRemovedEvent : DomainEvent
    {
        public Guid OrderId { get; }
        public Guid OrderItemId { get; }
        public Guid ProductId { get; }
        public Quantity Quantity { get; }

        public OrderItemRemovedEvent(
            Guid orderId,
            Guid orderItemId,
            Guid productId,
            Quantity quantity)
        {
            OrderId = orderId;
            OrderItemId = orderItemId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}