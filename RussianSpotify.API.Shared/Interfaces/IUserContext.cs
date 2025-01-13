namespace RussianSpotify.API.Shared.Interfaces;

/// <summary>
///     Контекс текущего пользоавтеля
/// </summary>
public interface IUserContext
{
    /// <summary>
    ///     ИД текущего пользователя
    /// </summary>
    Guid? CurrentUserId { get; }

    /// <summary>
    ///     Название роли текущего пользователя
    /// </summary>
    string? RoleName { get; }
}