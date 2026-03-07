using OrderManagement.Domain.Common;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Events
{
    public class OrderItemQuantityChangedEvent : DomainEvent
    {
        public Guid OrderId { get; }
        public Guid OrderItemId { get; }
        public Guid ProductId { get; }
        public Quantity OldQuantity { get; }
        public Quantity NewQuantity { get; }

        public OrderItemQuantityChangedEvent(
            Guid orderId,
            Guid orderItemId,
            Guid productId,
            Quantity oldQuantity,
            Quantity newQuantity)
        {
            OrderId = orderId;
            OrderItemId = orderItemId;
            ProductId = productId;
            OldQuantity = oldQuantity;
            NewQuantity = newQuantity;
        }
    }
}