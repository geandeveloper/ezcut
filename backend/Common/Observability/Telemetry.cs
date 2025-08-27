using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Common.Observability
{
    public class Telemetry : ITelemetry
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ActivitySource _activitySource;
        private readonly Meter _meter;

        public Telemetry(ILoggerFactory loggerFactory, ActivitySource activitySource, Meter meter)
        {
            _loggerFactory = loggerFactory;
            _activitySource = activitySource;
            _meter = meter;
        }

        public ITelemetryScope Begin(string operationName)
        {
            var logger = _loggerFactory.CreateLogger(operationName);
            return new TelemetryScope(operationName, logger, _activitySource, _meter);
        }
    }
}
