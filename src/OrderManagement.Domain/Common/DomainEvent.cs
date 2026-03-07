namespace OrderManagement.Domain.Common
{
    public abstract class DomainEvent
    {
        public DateTime OcurredOn { get; } = DateTime.UtcNow;

    }
}