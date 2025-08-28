using Common.Observability;

namespace Api.Barbers
{
    public readonly struct BarberMetrics
    {
        public MetricDefinition CreatedAttempt { get; }
        public MetricDefinition Success { get; }
        public MetricDefinition Failure { get; }

        public BarberMetrics()
        {
            CreatedAttempt = new("barber_creation_attempts", "Total attempts to create a barber");
            Success = new("barber_creation_successes", "Total successful barber creations");
            Failure = new("barber_creation_failures", "Total failed barber creation attempts");
        }
    }
}
