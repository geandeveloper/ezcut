using Common.Api;
using Common.Commands;

namespace Api.Barbers.Create
{
    public record CreateBarberCommand(RequestContext Context, Guid UserId, string Name, string Email) : Command<Guid>(Context);
}
