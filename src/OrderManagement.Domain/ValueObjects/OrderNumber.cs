namespace OrderManagement.Domain.ValueObjects
{
    public sealed class OrderNumber
    {
        public string Value { get; }


        public OrderNumber()
        {
            Value = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6]}";
        }

        public override string ToString() => Value;

        public override bool Equals(object? obj)
            => obj is OrderNumber o && Value == o.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}