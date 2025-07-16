using Common.Api;
using MediatR;

namespace Common.Commands
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        RequestContext Context { get; }
    }
}
