using Common.Api;

namespace Common.Commands
{
    public record Command<TResult>(RequestContext Context) : IRequest<TResult>;
}
