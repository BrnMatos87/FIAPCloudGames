using Infrastructure.Correlation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, ICorrelationIdAccessor correlationIdAccessor)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                var correlationId = correlationIdAccessor.Get();

                _logger.LogWarning(
    "Acesso não autorizado {Method} {Path} Message {Message} CorrelationId {CorrelationId}",
     context.Request.Method,
     context.Request.Path,
     ex.Message,
     correlationId);

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var json = JsonSerializer.Serialize(new
                {
                    message = ex.Message,
                    correlationId
                });

                await context.Response.WriteAsync(json);
            }
            catch (InvalidOperationException ex)
            {
                var correlationId = correlationIdAccessor.Get();

                _logger.LogWarning(                   
                   "Violação de regra de negócio {Method} {Path} CorrelationId {CorrelationId}",
                    context.Request.Method,
                    context.Request.Path,
                    ex.Message,
                    correlationId);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var json = JsonSerializer.Serialize(new
                {
                    message = ex.Message,
                    correlationId
                });

                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                var correlationId = correlationIdAccessor.Get();

                _logger.LogError(
                    ex,
                  "Erro desconhecido {Method} {Path} CorrelationId {CorrelationId}",
                    context.Request.Method,
                    context.Request.Path,
                    correlationId);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var json = JsonSerializer.Serialize(new
                {
                    message = "Erro inesperado.",
                    correlationId
                });

                await context.Response.WriteAsync(json);
            }
        }
    }

    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}