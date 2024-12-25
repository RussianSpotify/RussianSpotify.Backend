using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RussianSpotify.API.Core.Entities;

namespace RussianSpotify.API.Core.Abstractions;

/// <summary>
/// Интерфейс контекста бд
/// </summary>
public interface IDbContext
{
    /// <summary>
    /// Пользователи
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Роли
    /// </summary>
    public DbSet<Role> Roles { get; set; }

    /// <summary>
    /// Привилегии
    /// </summary>
    public DbSet<RolePrivilege> Privileges { get; set; }

    /// <summary>
    /// Альбомы
    /// </summary>
    public DbSet<Playlist> Playlists { get; set; }

    /// <summary>
    /// Песни
    /// </summary>
    public DbSet<Song> Songs { get; set; }

    /// <summary>
    /// Подписки
    /// </summary>
    public DbSet<Subscribe> Subscribes { get; set; }

    /// <summary>
    /// Категории
    /// </summary>
    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Корзины
    /// </summary>
    public DbSet<Bucket> Buckets { get; set; }

    /// <summary>
    /// Уведомления
    /// </summary>
    public DbSet<EmailNotification> EmailNotifications { get; set; }

    /// <summary>
    /// Чаты
    /// </summary>
    public DbSet<Chat> Chats { get; set; }

    /// <summary>
    /// Сообщения
    /// </summary>
    public DbSet<Message> Messages { get; set; }
    
    /// <summary>
    /// Сохранить изменения
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Кол-во затронутых записей</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Фасад базы
    /// </summary>
    public DatabaseFacade Database { get; }
}