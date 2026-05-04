using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Correlation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCorrelationIdAccessor(this IServiceCollection services)
        {
            services.AddScoped<ICorrelationIdAccessor, CorrelationIdAccessor>();
            return services;
        }
    }
}
