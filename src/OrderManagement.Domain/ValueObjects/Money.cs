namespace OrderManagement.Domain.ValueObjects
{
    public sealed class Money
    {
        public decimal Amount { get; }

        public Money(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("O valor não pode ser negativo.");

            Amount = amount;
        }

        public static Money Zero() => new(0);

        public static Money operator +(Money a, Money b)
            => new(a.Amount + b.Amount);

        public static Money operator *(Money money, int quantity)
            => new(money.Amount * quantity);
        public static Money operator -(Money a, Money b)
            => new(a.Amount - b.Amount);

        public static Money FromDecimal(decimal value)
        {
            return new Money(value);
        }

        public override string ToString() => Amount.ToString("C");
        public override int GetHashCode()
        {
            return Amount.GetHashCode();
        }
    }
}