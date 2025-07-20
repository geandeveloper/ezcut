using Common.Api;
using MediatR;

namespace Common.Commands
{
    public record Command<TResult>(RequestContext Context) : IRequest<TResult>;
}
