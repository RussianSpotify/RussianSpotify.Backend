using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Shared.Domain.Abstractions;

namespace RussianSpotify.API.Core.Entities;

/// <summary>
/// Сущность подписка
/// </summary>
public class Subscribe : BaseEntity, ISoftDeletable, ITimeTrackable
{
    /// <summary>
    /// Начало подписки
    /// </summary>
    public DateTime? DateStart { get; set; }

    /// <summary>
    /// Конец подписки
    /// </summary>
    public DateTime? DateEnd { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// ИД Пользователь
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <inheritdoc />
    public bool IsDeleted { get; set; }

    /// <inheritdoc />
    public DateTime? DeletedAt { get; set; }
    
    /// <inheritdoc />
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Тестовая сущность
    /// </summary>
    /// <param name="id">Ид</param>
    /// <param name="dateStart">Начало подписки</param>
    /// <param name="dateEnd">Конец подписки</param>
    /// <param name="user">Пользователь</param>
    /// <returns>Тестовая сущность</returns>
    [Obsolete("Только для тестов")]
    public static Subscribe CreateForTest(
        Guid? id = default,
        DateTime? dateStart = default,
        DateTime? dateEnd = default,
        User? user = default!)
        => new()
        {
            Id = id ?? default,
            DateStart = dateStart,
            DateEnd = dateEnd,
            User = user ?? new User("123", "123", "123"),
            UserId = user?.Id ?? default,
        };
}