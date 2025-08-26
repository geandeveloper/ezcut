using Common.Events;

namespace Common.Models
{
    public abstract class Aggregate
    {
        private readonly List<IDomainEvent> _uncommitted = [];

        public IReadOnlyCollection<IDomainEvent> UncommittedEvents => _uncommitted.AsReadOnly();

        public Guid Id { get; protected init; }
        public long Version { get; private set; } = -1;

        protected void RaiseEvent(IDomainEvent @event)
        {
            Apply(@event);
            _uncommitted.Add(@event);
        }

        public static TAggregate Raise<TAggregate>(IDomainEvent @event)
            where TAggregate : Aggregate, new()
        {
            var aggregate = new TAggregate();
            aggregate.RaiseEvent(@event);
            return aggregate;
        }

        public void MarkEventsAsCommitted() => _uncommitted.Clear();

        protected abstract void Apply(IDomainEvent @event);
    }
}
