using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Common.Observability
{
    public class TelemetryScope : ITelemetryScope
    {
        private readonly ILogger _logger;
        private readonly Activity? _activity;
        private readonly Meter _meter;

        public TelemetryScope(string name, ILogger logger, ActivitySource activitySource, Meter meter)
        {
            _logger = logger;
            _meter = meter;
            _activity = activitySource.StartActivity(name);
        }

        public ITelemetryScope Log(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
            return this;
        }

        public ITelemetryScope Metric(string metricName, double value, (string, object?)[] tags)
        {
            var kvTags = tags.Select(t => new KeyValuePair<string, object?>(t.Item1, t.Item2)).ToArray();
            var counter = _meter.CreateCounter<double>(metricName);
            
            counter.Add(value, kvTags);
            return this;
        }

        public ITelemetryScope Fail(Exception ex)
        {
            _logger.LogError(ex, "Erro na operação {Operation}", _activity?.DisplayName);
            _activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            return this;
        }

        public void Dispose()
        {
            _activity?.Dispose();
        }
    }
}
