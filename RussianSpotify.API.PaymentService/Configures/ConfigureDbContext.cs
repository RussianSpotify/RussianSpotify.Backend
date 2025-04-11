using RussianSpotify.API.PaymentService.Data;
using RussianSpotify.API.Shared.Data.PostgreSQL.Extensions;
using RussianSpotify.API.Shared.Data.PostgreSQL.Options;
using RussianSpotify.API.Shared.Data.PostgreSQL.Services;
using IMigrator = RussianSpotify.API.Shared.Interfaces.IMigrator;

namespace RussianSpotify.API.PaymentService.Configures;

public static class ConfigureDbContext
{
    /// <summary>
    /// Подключение бд
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="options">Настройки</param>
    public static void AddCustomDataContext(this IServiceCollection services, DbContextOptions options)
    {
        services.AddCustomDbContext<IDbContext, PaymentDbContext>(options.ConnectionString);
        services.AddTransient<IMigrator, Migrator<PaymentDbContext>>();
    }
}