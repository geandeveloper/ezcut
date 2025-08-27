using Api.Barbers.Create;
using Common.Commands;
using Common.Observability;

public class CreateBarberHandler : IRequestHandler<CreateBarberCommand, Guid>
{
    private static readonly Dictionary<Guid, CreateBarberCommand> _barbers = new();

    private readonly ITelemetry _telemetry;

    public CreateBarberHandler(ITelemetry telemetry)
    {
        _telemetry = telemetry;
    }

    public Task<Guid> Handle(CreateBarberCommand request, CancellationToken cancellationToken)
    {
        using var scope = _telemetry.Begin("CreateBarber")
            .Log("Iniciando criação do barber {Nome}", request.Name)
            .Metric("barbers_criados_tentativa", 1);

        try
        {
            var id = Guid.NewGuid();
            _barbers[id] = request;

            scope.Log("Barber {Id} criado com sucesso", id)
                 .Metric("barbers_criados_total", 1, ("status", "sucesso"));

            return Task.FromResult(id);
        }
        catch (Exception ex)
        {
            scope.Fail(ex)
                 .Metric("barbers_criados_total", 1, ("status", "falha"));

            throw;
        }
    }
}