using RussianSpotify.API.Shared.Data.PostgreSQL.Extensions;
using RussianSpotify.API.Shared.Data.PostgreSQL.Options;
using RussianSpotify.API.Shared.Data.PostgreSQL.Services;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.Grpc.SubscriptionService.Data;

namespace RussianSpotify.Grpc.SubscriptionService.Configures;

public static class ConfigureDbContext
{
    /// <summary>
    /// Регистрация уровня базы
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="options">Настройки</param>
    public static void AddDataContext(this IServiceCollection services, DbContextOptions options)
    {
        services.AddCustomDbContext<IDbContext, SubscriptionDbContext>(options.ConnectionString);
        services.AddTransient<IMigrator, Migrator<SubscriptionDbContext>>();
    }
}