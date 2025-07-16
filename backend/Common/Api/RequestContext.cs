namespace Common.Api
{
    public record RequestContext(Guid CorrelationId, Guid CausationId, DateTimeOffset DateTime)
    {
        public static RequestContext New(Guid? correlationId = null)
          => new(
              correlationId ?? Guid.NewGuid(),
              Guid.NewGuid(),
              DateTimeOffset.UtcNow);
    }
}
