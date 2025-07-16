using Microsoft.AspNetCore.Http;

namespace Common.Api.Middleware
{
    public class RequestContextMiddleware(RequestDelegate next)
    {
        private const string HeaderKey = "X-Correlation-ID";
        public const string HttpContextItemKey = nameof(RequestContext);

        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            var correlationId = context.Request.Headers.TryGetValue(HeaderKey, out var headerVal) && Guid.TryParse(headerVal, out var parsedId)
                ? parsedId
                : Guid.NewGuid();

            var requestContext = RequestContext.New(correlationId);
            context.Items[HttpContextItemKey] = requestContext;

            await _next(context);
        }
    }
}
