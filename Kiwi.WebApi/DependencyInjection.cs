using Kiwi.WebApi.Infrastructure;

namespace Kiwi.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        
        return services;
    }
}