namespace RussianSpotify.API.Core.Entities;

/// <summary>
/// Сущность подписка
/// </summary>
public class Subscribe
{
    /// <summary>
    /// ИД подписки
    /// </summary>
    public Guid Id { get; protected set; }

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
        User? user = default)
        => new()
        {
            Id = id ?? default,
            DateStart = dateStart,
            DateEnd = dateEnd,
            User = user ?? new User(),
            UserId = user?.Id ?? default,
        };

}