namespace OrderManagement.Domain.ValueObjects
{
    public sealed class Money
    {
        public decimal Amount { get; }

        private Money() { }

        public Money(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("O valor não pode ser negativo.");

            Amount = amount;
        }

        public static Money Zero() => new(0);

        public static Money FromDecimal(decimal value) => new(value);

        public static Money operator +(Money a, Money b)
            => new(a.Amount + b.Amount);

        public static Money operator *(Money money, int quantity)
            => new(money.Amount * quantity);

        public static Money operator -(Money a, Money b)
        {
            var result = a.Amount - b.Amount;

            if (result < 0)
                throw new InvalidOperationException("Resultado não pode ser negativo.");

            return new Money(result);
        }

        public override string ToString() => Amount.ToString("C");

        public override bool Equals(object? obj)
            => obj is Money money && Amount == money.Amount;

        public override int GetHashCode() => Amount.GetHashCode();
    }
}