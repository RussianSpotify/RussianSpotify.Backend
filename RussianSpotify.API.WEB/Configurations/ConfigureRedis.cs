namespace RussianSpotify.API.WEB.Configurations;

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
        // TODO: Перевести на паттерн Options
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
            options.InstanceName = "Redis";
        });
    }
}