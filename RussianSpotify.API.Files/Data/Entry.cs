using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Shared.Data.PostgreSQL.Extensions;
using RussianSpotify.API.Shared.Data.PostgreSQL.Options;
using RussianSpotify.API.Shared.Data.PostgreSQL.Services;
using DbContextOptions = RussianSpotify.API.Shared.Data.PostgreSQL.Options.DbContextOptions;
using IMigrator = RussianSpotify.API.Shared.Interfaces.IMigrator;

namespace RussianSpotify.API.Files.Data;

/// <summary>
/// Входная точка
/// </summary>
public static class Entry
{
    /// <summary>
    /// Регистрация зависимостей
    /// </summary>
    public static void AddDataContext(this IServiceCollection serviceCollection, DbContextOptions options)
    {
        if (serviceCollection is null)
            throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddCustomDbContext<IDbContext, FileDbContext>(options.ConnectionString);
        serviceCollection.AddTransient<IMigrator, Migrator<FileDbContext>>();
    }
}