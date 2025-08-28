using Api.Barbers;
using Common.Commands;
using Common.Observability;

namespace Api.Barbers.Create
{
    public class CreateBarberHandler(ITelemetry telemetry) : IRequestHandler<CreateBarberCommand, Guid>
    {
        private static readonly Dictionary<Guid, CreateBarberCommand> _barbers = new();

        public Task<Guid> Handle(CreateBarberCommand request, CancellationToken cancellationToken)
        {
            var metrics = new BarberMetrics();

            using var scope = telemetry.Begin("CreateBarber")
                .Log("Starting barber creation: {Name}", request.Name)
                .Metric<BarberMetrics>(m => m.CreatedAttempt, 1);

            try
            {
                if ((new Random().Next(30)) % 2 == 0)
                    throw new Exception("without lucky");

                var id = Guid.NewGuid();
                _barbers[id] = request;

                scope.Log("Barber {Id} successfully created", id)
                     .Metric<BarberMetrics>(m => m.Success, 1);

                return Task.FromResult(id);
            }
            catch (Exception ex)
            {
                scope.Fail(ex)
                     .Metric<BarberMetrics>(m => m.Failure, 1, ("exception", ex.GetType().Name));

                throw;
            }
        }
    }
}