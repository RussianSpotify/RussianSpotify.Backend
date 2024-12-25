using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RussianSpotify.API.Shared.Data.PostgreSQL.Interceptors;

namespace RussianSpotify.API.Shared.Data.PostgreSQL.Extensions;

public static class AddDataContextExtension
{
    /// <summary>
    /// Добавление db контекста
    /// </summary>
    /// <param name="services">Коллекция сервисов билдера</param>
    /// <param name="connectionString">Cтрока подключения</param>
    public static void AddCustomDbContext<TIContext, TContext>(this IServiceCollection services, string connectionString) 
        where TContext : DbContext, TIContext =>
        services.AddDbContext<TIContext, TContext>(
            (sp, options) => options
                .UseNpgsql(connectionString)
                // .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>())
                .AddInterceptors(sp.GetRequiredService<UpdateInterceptor>()));
}