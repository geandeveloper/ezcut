namespace Common.Observability
{
    public interface ITelemetry
    {
        ITelemetryScope Begin(string operationName);
    }
}
