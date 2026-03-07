using OrderManagement.Domain.Common;

namespace OrderManagement.Domain.Events
{
    public class OrderShippedEvent : DomainEvent
    {
        public Guid OrderId { get; }

        public OrderShippedEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}