using OrderManagement.Domain.Common;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; private set; }

        public Guid ProductId { get; private set; }

        public Quantity Quantity { get; private set; }

        public Money UnitPrice { get; private set; }

        public Money Total => UnitPrice * Quantity.Value;

        // private OrderItem() { }

        public OrderItem(Guid orderId, Guid productId, Quantity quantity, Money unitPrice)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException("Pedido inválido.");

            if (productId == Guid.Empty)
                throw new ArgumentException("Produto inválido.");

            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        internal void UpdateQuantity(Quantity quantity)
        {
            Quantity = quantity;
        }

        internal void IncreaseQuantity(Quantity quantity)
        {
            Quantity = new Quantity(Quantity.Value + quantity.Value);
        }
    }
}