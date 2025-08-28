using System.Linq.Expressions;

namespace Common.Observability
{
    public interface ITelemetryScope : IDisposable
    {
        ITelemetryScope Log(string message, params object[] args);

        ITelemetryScope Metric<TMetrics>(Expression<Func<TMetrics, MetricDefinition>> metric, double value, params (string key, object? value)[] tags)
            where TMetrics : new();
        ITelemetryScope Fail(Exception ex);
        void Dispose();
    }
}
