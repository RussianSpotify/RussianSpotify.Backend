#region

using Microsoft.Extensions.DependencyInjection;
using RussianSpotift.API.Data.PostgreSQL.Seeder;
using RussianSpotify.API.Core.Abstractions;

#endregion

namespace RussianSpotift.API.Data.PostgreSQL;

/// <summary>
///     Входная точка
/// </summary>
public static class Entry
{
    /// <summary>
    ///     Регистрация зависимостей
    /// </summary>
    public static void AddPostgreSqlLayout(this IServiceCollection serviceCollection)
    {
        if (serviceCollection is null)
            throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Entry).Assembly));
        serviceCollection.AddScoped<IDbSeeder, DbSeeder>();
        serviceCollection.AddScoped<IDbContext, EfContext>();
        serviceCollection.AddTransient<Migrator>();
        serviceCollection.AddLogging();
        serviceCollection.AddHttpContextAccessor();
    }
}