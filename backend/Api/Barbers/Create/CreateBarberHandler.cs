
using Common.Commands;

namespace Api.Barbers.Create
{
    public class CreateBarberHandler : IRequestHandler<CreateBarberCommand, Guid>
    {
        public Task<Guid> Handle(CreateBarberCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Guid.Empty);
        }
    }
}
