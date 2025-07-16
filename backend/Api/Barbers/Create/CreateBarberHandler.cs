using MediatR;

namespace Api.Barbers.Create
{
    public class CreateBarberHandler : IRequestHandler<CreateBarberCommand, Guid>
    {
        public Task<Guid> Handle(CreateBarberCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
