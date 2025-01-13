#region

using Microsoft.EntityFrameworkCore;
using RussianSpotift.API.Data.PostgreSQL;
using RussianSpotify.API.Shared.Data.PostgreSQL.Interceptors;

#endregion

namespace RussianSpotify.API.WEB.Configurations;

/// <summary>
///     Конфигурация бд
/// </summary>
public static class ConfigureDbContext
{
    /// <summary>
    ///     Добавление db контекста
    /// </summary>
    /// <param name="services">Коллекция сервисов билдера</param>
    /// <param name="connectionString">Cтрока подключения</param>
    public static void AddCustomDbContext(this IServiceCollection services, string connectionString) =>
        services.AddDbContext<EfContext>(
            (sp, options) => options
                .UseNpgsql(connectionString)
                // .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>())
                .AddInterceptors(sp.GetRequiredService<UpdateInterceptor>()));
}