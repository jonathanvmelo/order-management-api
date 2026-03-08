using OrderManagement.Domain.Common;

public class OrderProcessingStartedEvent : DomainEvent
{
    public Guid OrderId { get; }
    public Guid CustomerId { get; }

    public OrderProcessingStartedEvent(Guid orderId, Guid customerId)
    {
        OrderId = orderId;
        CustomerId = customerId;
    }
}