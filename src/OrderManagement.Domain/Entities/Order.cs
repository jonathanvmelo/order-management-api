using OrderManagement.Domain.Common;
using OrderManagement.Domain.Enums;

namespace OrderManagement.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; private set; }

        public OrderStatus Status { get; private set; }

        public decimal TotalAmount { get; private set; }

        public Order(Guid customerId)
        {
            CustomerId = customerId;
            Status = OrderStatus.Pending;
            TotalAmount = 0;
        }


        public void Cancel()
        {
            if (Status is OrderStatus.Cancelled)
                throw new InvalidOperationException("Order is already cancelled.");
            if (Status is OrderStatus.Shipped || Status is OrderStatus.Delivered)
                throw new InvalidOperationException("Order cannot be cancelled after shipment.");

            Status = OrderStatus.Cancelled;
        }

        public void UpdateTotal(decimal amount)
        {
            if (Status is OrderStatus.Shipped || Status is OrderStatus.Delivered)
                throw new InvalidOperationException("Cannot change total after shipment.");
            if (amount < 0)
                throw new ArgumentException("Total amount cannot be negative.");

            TotalAmount = amount;
        }

        public void MarkAsPaid()
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Only pending orders can be paid.");

            Status = OrderStatus.Paid;
        }
        public void StartProcessing()
        {
            if (Status != OrderStatus.Paid)
                throw new InvalidOperationException("Order must be paid before processing.");

            Status = OrderStatus.Processing;
        }
        public void Ship()
        {
            if (Status != OrderStatus.Processing)
                throw new InvalidOperationException("Order must be processing before shipping.");

            Status = OrderStatus.Shipped;
        }
        public void Deliver()
        {
            if (Status != OrderStatus.Shipped)
                throw new InvalidOperationException("Order must be shipped before delivery.");

            Status = OrderStatus.Delivered;
        }
    }
}