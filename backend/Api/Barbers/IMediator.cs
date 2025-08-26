using Api.Barbers.Create;

namespace Api.Barbers
{
    internal interface IMediator
    {
        Task<Guid> Send(CreateBarberCommand cmd);
    }

    internal class Mediator : IMediator
    {
        public async Task<Guid> Send(CreateBarberCommand cmd)
        {
            var handler = new CreateBarberHandler().Handle(cmd, CancellationToken.None);
            return await handler;
        }
    }
}