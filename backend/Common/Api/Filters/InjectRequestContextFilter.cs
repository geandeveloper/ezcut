using Common.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Common.Api.Filters;

public class InjectRequestContextFilter<TCommand> : IEndpointFilter
    where TCommand : Command<Guid>
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {

        for (int i = 0; i < context.Arguments.Count; i++)
        {
            if (context.Arguments[i] is TCommand command)
            {
                context.Arguments[i] = command with { Context = RequestContext.New() };

                break;
            }

        }

        return await next(context);
    }

}


public static class InjectRequestContextFilterExtensions
{
    public static RouteHandlerBuilder AddRequestContextCommandFilter<TCommand>(this RouteHandlerBuilder builder)
        where TCommand : Command<Guid>
        => builder.AddEndpointFilter<InjectRequestContextFilter<TCommand>>();
}

