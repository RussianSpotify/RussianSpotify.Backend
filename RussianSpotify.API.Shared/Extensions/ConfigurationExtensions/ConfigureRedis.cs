using Microsoft.Extensions.DependencyInjection;
using RussianSpotify.API.Shared.Options;

namespace RussianSpotify.API.Shared.Extensions.ConfigurationExtensions;

/// <summary>
/// Конфигурация Redis
/// </summary>
public static class ConfigureRedis
{
    /// <summary>
    /// Добавить Redis
    /// </summary>
    /// <param name="serviceCollection">Сервисы</param>
    /// <param name="options">Конфигурация</param>
    public static void AddRedis(this IServiceCollection serviceCollection, RedisOptions options)
    {
        if (serviceCollection is null)
            throw new ArgumentNullException(nameof(serviceCollection));

        if (options == null)
            throw new InvalidOperationException("Redis settings are not configured. Please provide a 'Redis' section in the configuration.");

        serviceCollection.AddStackExchangeRedisCache(redisCacheOptions =>
        {
            redisCacheOptions.Configuration = options.ConnectionString;
        });
    }
}