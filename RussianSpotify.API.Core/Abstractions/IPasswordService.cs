using RussianSpotify.API.Core.Entities;

namespace RussianSpotify.API.Core.Abstractions;

/// <summary>
/// Сервис для работы с паролями
/// </summary>
public interface IPasswordService
{
    /// <summary>
    /// Захешировать пароль
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <returns>Хеш</returns>
    public string HashPassword(string password);

    /// <summary>
    /// Проверить пароль
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="hash">Хеш пароля</param>
    /// <returns>Верны ли</returns>
    public bool VerifyPassword(string password, string hash);
}