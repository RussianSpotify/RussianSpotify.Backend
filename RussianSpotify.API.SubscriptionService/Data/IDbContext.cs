using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RussianSpotify.Grpc.SubscriptionService.Domain.Entities;

namespace RussianSpotify.Grpc.SubscriptionService.Data;

public interface IDbContext
{
    /// <summary>
    /// Подписки
    /// </summary>
    public DbSet<Subscription> Subscriptions { get; set; }

    /// <summary>
    /// Сообщения outbox
    /// </summary>
    public DbSet<MessageOutbox> MessageOutboxes { get; set; }
    
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