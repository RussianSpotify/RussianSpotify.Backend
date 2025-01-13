#region

using RussianSpotify.API.Core.Entities;

#endregion

namespace RussianSpotify.API.Core.Abstractions;

/// <summary>
///     Сервис по смене пароля
/// </summary>
public interface IPasswordChanger
{
    /// <summary>
    ///     Поменять пароль
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <param name="oldPassword">Старый пароль</param>
    /// <param name="newPassword">Новый пароль</param>
    public void ChangePassword(User user, string oldPassword, string newPassword);
}