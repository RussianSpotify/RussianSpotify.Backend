using RussianSpotify.API.Core.Enums;
using RussianSpotify.API.Shared.Enums;

namespace RussianSpotify.API.Core.Entities;

/// <summary>
/// Сущность подписки из другой бд
/// </summary>
public class ExternalSubscription
{
    public Guid Id { get; set; }
    
    public string? FailedReason { get; set; }
    
    /// <summary>
    ///     Начало подписки
    /// </summary>
    public DateTime? DateStart { get; set; }

    /// <summary>
    ///     Конец подписки
    /// </summary>
    public DateTime? DateEnd { get; set; }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Статус подписки
    /// </summary>
    public SubscriptionStatus Status { get; set; }
    
    /// <summary>
    /// Удален ли
    /// </summary>
    public bool IsDeleted { get; set; }
    
    /// <summary>
    /// Дата удаления
    /// </summary>
    public DateTime? DeletedAt { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Дата обновления
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}