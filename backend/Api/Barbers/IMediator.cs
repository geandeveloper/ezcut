using Api.Barbers.Create;

namespace Api.Barbers
{
    internal interface IMediator
    {
        Task<Guid> Send(CreateBarberCommand cmd);
    }

    internal class Mediator(CreateBarberHandler handler) : IMediator
    {
        private readonly CreateBarberHandler _handler = handler;

        public Task<Guid> Send(CreateBarberCommand cmd)
        {
            return _handler.Handle(cmd, CancellationToken.None);
        }
    }
}