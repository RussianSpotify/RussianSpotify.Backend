using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RussianSpotify.API.Core.Entities;

namespace RussianSpotify.API.Core.Abstractions;

public interface IExternalSubscriptionDbContext
{
    /// <summary>
    /// Таблица подписок из базы Подписок
    /// </summary>
    public DbSet<ExternalSubscription> ExternalSubscriptions { get; set; }
    
    /// <summary>
    ///     Сохранить изменения
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Кол-во затронутых записей</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Фасад базы
    /// </summary>
    public DatabaseFacade Database { get; }
}