using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

namespace Common.Observability
{
    public class TelemetryScope(string name, ILogger logger, ActivitySource activitySource, Meter meter) : ITelemetryScope, IDisposable
    {
        private readonly Activity? _activity = activitySource?.StartActivity(name);
        private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly Meter _meter = meter ?? throw new ArgumentNullException(nameof(meter));

        private static readonly ConcurrentDictionary<string, Counter<double>> _counters = new();

        public ITelemetryScope Log(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
            return this;
        }

        public ITelemetryScope Metric<TMetrics>(Expression<Func<TMetrics, MetricDefinition>> metric, double value, params (string key, object? value)[] tags
        ) where TMetrics : new()
        {
            var metrics = new TMetrics();
            var metricDef = metric.Compile().Invoke(metrics);

            var counter = _counters.GetOrAdd(metricDef.Name,
                name => _meter.CreateCounter<double>(name));

            var kvTags = tags
                .Select(t => new KeyValuePair<string, object?>(t.key, t.value))
                .ToArray();

            counter.Add(value, kvTags);

            return this;
        }

        public ITelemetryScope Fail(Exception ex)
        {
            _logger.LogError(ex, "Operation Error: {Operation}", _activity?.DisplayName);
            _activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            return this;
        }

        public void Dispose()
        {
            _activity?.Dispose();
        }
    }
}
