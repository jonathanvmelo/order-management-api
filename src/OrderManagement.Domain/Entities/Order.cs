using OrderManagement.Domain.Common;
using OrderManagement.Domain.Enums;

namespace OrderManagement.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; private set; }

        public OrderStatus Status { get; private set; }

        public decimal TotalAmount { get; private set; }
    }
}