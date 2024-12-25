using Microsoft.Extensions.DependencyInjection;
using RussianSpotify.API.Shared.Extensions.ConfigurationExtensions.CorsPolicy;

namespace RussianSpotify.API.Shared.Extensions.ConfigurationExtensions;

/// <summary>
/// Конфигурация Cors
/// </summary>
public static class ConfigureCors
{
    /// <summary>
    /// Добавить политики
    /// </summary>
    /// <param name="serviceCollection">Сервисы</param>
    public static void AddCustomCors(this IServiceCollection serviceCollection)
        => serviceCollection.AddCors(
            corsOptions => corsOptions
                .AddPolicy(CorsPolicyConstants.AllowAll, policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                })
        );
}