using Infrastructure.Correlation;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Logging
{
    public class BaseLogger<T>
    {
        protected readonly ILogger<T> _logger;
        protected readonly ICorrelationIdAccessor _correlationIdAccessor;

        public BaseLogger(ILogger<T> logger, ICorrelationIdAccessor correlationIdAccessor)
        {
            _logger = logger;
            _correlationIdAccessor = correlationIdAccessor;
        }

        public virtual void LogInformation(string message)
        {
            _logger.LogInformation(
                "{Message} CorrelationId {CorrelationId}",
                message,
                _correlationIdAccessor.Get());
        }

        public virtual void LogWarning(string message)
        {
            _logger.LogWarning(
                "{Message} CorrelationId {CorrelationId}",
                message,
                _correlationIdAccessor.Get());
        }

        public virtual void LogError(string message)
        {
            _logger.LogError(
                "{Message} CorrelationId {CorrelationId}",
                message,
                _correlationIdAccessor.Get());
        }

        public virtual void LogError(Exception exception, string message)
        {
            _logger.LogError(
                exception,
                "{Message} CorrelationId {CorrelationId}",
                message,
                _correlationIdAccessor.Get());
        }
    }
}
