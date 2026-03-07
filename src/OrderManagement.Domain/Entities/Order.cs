using OrderManagement.Domain.Common;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Events;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Entities
{
    public class Order : BaseEntity
    {
        private readonly List<OrderItem> _items = [];
        private readonly List<DomainEvent> _events = [];
        public Guid CustomerId { get; private set; }

        public OrderNumber OrderNumber { get; private set; }

        public OrderStatus Status { get; private set; }

        public Money TotalAmount { get; private set; }

        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public IReadOnlyCollection<DomainEvent> Events => _events.AsReadOnly();


        public Order(Guid customerId)
        {
            CustomerId = customerId;
            OrderNumber = new OrderNumber();
            Status = OrderStatus.Pending;
            TotalAmount = Money.Zero();

            AddEvent(new OrderCreatedEvent(Id, customerId));
        }


        public void AddItem(Guid productId, Quantity quantity, Money unitPrice)
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Não é possível modificar um pedido processado.");

            var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem is not null)
            {
                existingItem.IncreaseQuantity(quantity);
            }
            else
            {
                var item = new OrderItem(Id, productId, quantity, unitPrice);
                _items.Add(item);
            }

            RecalculateTotal();
        }

        public void RemoveItem(Guid productId)
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Não é possível remover itens após o pedido ser processado.");

            var item = _items.FirstOrDefault(i => i.ProductId == productId);

            if (item is null)
                throw new InvalidOperationException("Item não encontrado no pedido.");

            _items.Remove(item);

            RecalculateTotal();
        }

        public void UpdateItemQuantity(Guid productId, int quantity)
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Não é possível alterar itens após o pedido ser processado.");

            if (quantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            var item = _items.FirstOrDefault(i => i.ProductId == productId);

            if (item is null)
                throw new InvalidOperationException("Item não encontrado no pedido.");

            item.UpdateQuantity(new Quantity(quantity));

            RecalculateTotal();
        }


        public void Cancel()
        {
            if (Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("O pedido já está cancelado.");

            if (Status == OrderStatus.Shipped || Status == OrderStatus.Delivered)
                throw new InvalidOperationException("Não é possível cancelar um pedido após o envio.");

            Status = OrderStatus.Cancelled;
        }

        public void MarkAsPaid()
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Apenas pedidos pendentes podem ser pagos.");

            if (!_items.Any())
                throw new InvalidOperationException("Não é possível pagar um pedido sem itens.");

            Status = OrderStatus.Paid;
        }

        public void StartProcessing()
        {
            if (Status != OrderStatus.Paid)
                throw new InvalidOperationException("O pedido deve estar pago antes de iniciar o processamento.");

            Status = OrderStatus.Processing;
        }

        public void Ship()
        {
            if (Status != OrderStatus.Processing)
                throw new InvalidOperationException("O pedido deve estar em processamento antes do envio.");

            Status = OrderStatus.Shipped;
        }

        public void Deliver()
        {
            if (Status != OrderStatus.Shipped)
                throw new InvalidOperationException("O pedido deve estar enviado antes da entrega.");

            Status = OrderStatus.Delivered;
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
        private void AddEvent(DomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }
        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}