namespace Common.Observability
{
    public interface ITelemetryScope : IDisposable
    {
        ITelemetryScope Log(string message, params object[] args);
        ITelemetryScope Metric(string metricName, double value, params (string key, object? value)[] tags);
        ITelemetryScope Fail(Exception ex);
    }
}
