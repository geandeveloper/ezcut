using Common.Api;
using Common.Commands;

namespace Api.Barbers.Create
{
    public sealed record CreateBarberCommand(Guid UserId, string Name, string Email, RequestContext Context) : ICommand<Guid>;
}
