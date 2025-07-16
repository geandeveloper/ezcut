using Common.Api;
using MediatR;

namespace Common.Events
{
    public interface IDomainEvent : INotification
    {
        Guid AggregateId { get; }
        long Version { get; set; }
        RequestContext Context { get; }
        DateTimeOffset OccurredOn { get; }
    }
}
