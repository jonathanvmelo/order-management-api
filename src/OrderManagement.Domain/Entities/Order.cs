using OrderManagement.Domain.Common;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Events;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Entities
{
    public class Order : BaseEntity
    {
        private readonly List<OrderItem> _items = [];

        public Guid CustomerId { get; private set; }

        public OrderNumber OrderNumber { get; private set; }

        public OrderStatus Status { get; private set; }

        public Money TotalAmount { get; private set; }

        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        // private Order() { } // EF

        public Order(Guid customerId)
        {
            if (customerId == Guid.Empty)
                throw new ArgumentException("Cliente inválido.");

            CustomerId = customerId;
            OrderNumber = new OrderNumber();
            Status = OrderStatus.Pending;
            TotalAmount = Money.Zero();

            AddEvent(new OrderCreatedEvent(Id, customerId));
        }

        public void AddItem(Guid productId, Quantity quantity, Money unitPrice)
        {
            EnsurePending();

            if (productId == Guid.Empty)
                throw new ArgumentException("Produto inválido.");

            var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem is not null)
            {
                var oldQuantity = existingItem.Quantity;

                existingItem.IncreaseQuantity(quantity);

                AddEvent(new OrderItemQuantityChangedEvent(
                    Id,
                    existingItem.Id,
                    productId,
                    oldQuantity,
                    existingItem.Quantity
                ));
            }
            else
            {
                var item = new OrderItem(Id, productId, quantity, unitPrice);

                _items.Add(item);

                AddEvent(new OrderItemAddedEvent(
                    Id,
                    item.Id,
                    productId,
                    quantity,
                    unitPrice
                ));
            }

            RecalculateTotal();
        }

        public void RemoveItem(Guid productId)
        {
            EnsurePending();

            var item = _items.FirstOrDefault(i => i.ProductId == productId);

            if (item is null)
                throw new InvalidOperationException("Item não encontrado no pedido.");

            _items.Remove(item);

            AddEvent(new OrderItemRemovedEvent(
                Id,
                item.Id,
                item.ProductId,
                item.Quantity
            ));

            RecalculateTotal();
        }

        public void UpdateItemQuantity(Guid productId, Quantity quantity)
        {
            EnsurePending();

            var item = _items.FirstOrDefault(i => i.ProductId == productId);

            if (item is null)
                throw new InvalidOperationException("Item não encontrado no pedido.");

            var oldQuantity = item.Quantity;

            item.UpdateQuantity(quantity);

            AddEvent(new OrderItemQuantityChangedEvent(
                Id,
                item.Id,
                productId,
                oldQuantity,
                quantity
            ));

            RecalculateTotal();
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("O pedido já está cancelado.");

            if (Status == OrderStatus.Shipped || Status == OrderStatus.Delivered)
                throw new InvalidOperationException("Não é possível cancelar um pedido após o envio.");

            Status = OrderStatus.Cancelled;

            AddEvent(new OrderCancelledEvent(Id));
        }

        public void MarkAsPaid()
        {
            EnsurePending();

            if (!_items.Any())
                throw new InvalidOperationException("Não é possível pagar um pedido sem itens.");

            Status = OrderStatus.Paid;

            AddEvent(new OrderPaidEvent(Id));
        }

        public void StartProcessing()
        {
            if (Status != OrderStatus.Paid)
                throw new InvalidOperationException("O pedido deve estar pago antes de iniciar o processamento.");

            Status = OrderStatus.Processing;

            AddEvent(new OrderProcessingStartedEvent(Id, CustomerId));
        }

        public void Ship()
        {
            if (Status != OrderStatus.Processing)
                throw new InvalidOperationException("O pedido deve estar em processamento antes do envio.");

            Status = OrderStatus.Shipped;

            AddEvent(new OrderShippedEvent(Id));
        }

        public void Deliver()
        {
            if (Status != OrderStatus.Shipped)
                throw new InvalidOperationException("O pedido deve estar enviado antes da entrega.");

            Status = OrderStatus.Delivered;

            AddEvent(new OrderDeliveredEvent(Id));
        }

        private void RecalculateTotal()
        {
            var total = Money.Zero();

            foreach (var item in _items)
            {
                total += item.Total;
            }

            TotalAmount = total;
        }

        private void EnsurePending()
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Itens só podem ser modificados em pedidos pendentes.");
        }
    }
}