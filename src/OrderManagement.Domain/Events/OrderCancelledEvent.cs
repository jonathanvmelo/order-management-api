using OrderManagement.Domain.Common;

namespace OrderManagement.Domain.Events
{
    public class OrderCancelledEvent : DomainEvent
    {
        public Guid OrderId { get; }

        public OrderCancelledEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}