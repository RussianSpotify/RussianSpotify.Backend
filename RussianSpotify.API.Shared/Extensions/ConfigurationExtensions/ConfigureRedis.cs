using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RussianSpotify.API.Shared.Extensions.ConfigurationExtensions;

/// <summary>
/// Конфигурация Redis
/// </summary>
public static class ConfigureRedis
{
    /// <summary>
    /// Добавить Redis
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="configuration">Конфигурация</param>
    public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        var redisConnectionString = configuration.GetConnectionString("RedisConnection");

        if (redisConnectionString == null)
        {
            throw new InvalidOperationException("Redis settings are not configured. Please provide a 'Redis' section in the configuration.");
        }

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;
        });
    }
}