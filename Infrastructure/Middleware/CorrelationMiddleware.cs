using Infrastructure.Correlation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middleware
{
    public class CorrelationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CorrelationIdHeader = "x-correlation-id";

        public CorrelationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ICorrelationIdAccessor correlationIdAccessor)
        {
            var correlationId = GetOrCreateCorrelationId(context);

            correlationIdAccessor.Set(correlationId);

            // garante que a request também tenha o header
            context.Request.Headers[CorrelationIdHeader] = correlationId;

            context.Response.OnStarting(() =>
            {
                context.Response.Headers[CorrelationIdHeader] = correlationId;
                return Task.CompletedTask;
            });

            await _next(context);
        }

        private static string GetOrCreateCorrelationId(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId) &&
                Guid.TryParse(correlationId, out _))
            {
                return correlationId.ToString();
            }

            return Guid.NewGuid().ToString();
        }
    }

    public static class CorrelationMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorrelationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorrelationMiddleware>();
        }
    }
}