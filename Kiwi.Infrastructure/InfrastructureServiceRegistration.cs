using Kiwi.Core.Interfaces.Content;
using Kiwi.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kiwi.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DataContext>();
        services.AddTransient<IContentRepository, ContentRepository>();

        return services;
    }
}