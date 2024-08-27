using RussianSpotify.API.Core.Abstractions;

namespace RussianSpotify.API.Core.Services;

/// <summary>
/// Сервис паролей
/// </summary>
public class PasswordService : IPasswordService
{
    /// <inheritdoc />
    public string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    /// <inheritdoc />
    public bool VerifyPassword(string password, string hash)
        => BCrypt.Net.BCrypt.Verify(text: password, hash: hash);
}