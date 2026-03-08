namespace OrderManagement.Domain.ValueObjects
{
    public sealed class Quantity
    {
        public int Value { get; }

        private Quantity() { }

        public Quantity(int value)
        {
            if (value <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            Value = value;
        }

        public static implicit operator int(Quantity q) => q.Value;

        public override bool Equals(object? obj)
            => obj is Quantity q && Value == q.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}