#region

using FluentValidation;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;

#endregion

namespace RussianSpotify.API.Core.Services;

/// <summary>
///     Сервис по смене пароля
/// </summary>
public class PasswordChanger : IPasswordChanger
{
    private readonly IPasswordService _passwordService;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="passwordService">Сервис по хешированию</param>
    public PasswordChanger(IPasswordService passwordService)
        => _passwordService = passwordService;

    /// <inheritdoc />
    public void ChangePassword(User user, string oldPassword, string newPassword)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (string.IsNullOrWhiteSpace(oldPassword))
            throw new ArgumentNullException(oldPassword);

        if (string.IsNullOrWhiteSpace(newPassword))
            throw new ArgumentNullException(newPassword);

        var isEqual = _passwordService.VerifyPassword(oldPassword, user.PasswordHash);

        if (!isEqual)
            throw new ValidationException("Пароль неверный");

        var newHash = _passwordService.HashPassword(newPassword);

        user.PasswordHash = newHash;
    }
}