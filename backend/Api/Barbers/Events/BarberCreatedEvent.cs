using Common.Api;
using Common.Events;

namespace Api.Barbers.Events
{
    public sealed record BarberCreatedEvent(
        Guid AggregateId,
        Guid UserId,
        string Name,
        string Email,
        RequestContext Context) : IDomainEvent
    {
        public long Version { get; set; }
        public DateTimeOffset OccurredOn { get; init; } = DateTimeOffset.UtcNow;
    }
}
