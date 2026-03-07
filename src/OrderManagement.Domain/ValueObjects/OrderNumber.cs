namespace OrderManagement.Domain.ValueObjects
{
    public sealed class OrderNumber
    {
        public string Value { get; }

        public OrderNumber()
        {
            Value = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6]}";
        }
        public override string ToString()
        {
            return Value;
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

    }

}