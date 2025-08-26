using Microsoft.Extensions.Logging;

namespace Common.Events
{
    public abstract class EventHandler<TEvent> : INotificationHandler<TEvent>
      where TEvent : IDomainEvent
    {
        private readonly ILogger _logger;
        protected EventHandler(ILogger logger) => _logger = logger;

        public async Task Handle(TEvent @event, CancellationToken ct)
        {
            _logger.LogInformation($"Handling {@event} – CorrelationId: {@event.Context.CorrelationId}, CausationId: {@event.Context.CausationId}",
                typeof(TEvent).Name,
                @event.Context.CorrelationId,
                @event.Context.CausationId);

            await OnEvent(@event, ct);
        }

        protected abstract Task OnEvent(TEvent @event, CancellationToken ct);
    }
}
