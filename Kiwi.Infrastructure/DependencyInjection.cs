using Kiwi.Application.Interfaces;
using Kiwi.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kiwi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DataContext>();
        services.AddTransient<IContentRepository, ContentRepository>();

        return services;
    }
}