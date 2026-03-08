namespace OrderManagement.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public Guid CreatedBy { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public Guid? UpdatedBy { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }
        public Guid? DeletedBy { get; protected set; }
        public bool IsDeleted { get; protected set; }
        private readonly List<DomainEvent> _events = [];

        public IReadOnlyCollection<DomainEvent> Events => _events.AsReadOnly();

        protected void AddEvent(DomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }

        public void MarkAsUpdated(Guid userId)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = userId;
        }

        public void MarkAsDeleted(Guid userId)
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            DeletedBy = userId;
        }
    }
}