namespace Common.Events
{
    public interface INotificationHandler<TEvent> where TEvent : IDomainEvent
    {
    }
}