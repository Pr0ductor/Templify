using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Templify.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // Здесь будут регистрироваться сервисы инфраструктуры
        // Например, внешние API, email сервисы и т.д.
        
        return services;
    }
}
