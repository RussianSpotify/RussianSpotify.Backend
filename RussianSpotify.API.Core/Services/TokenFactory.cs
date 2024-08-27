using RussianSpotify.API.Core.Abstractions;

namespace RussianSpotify.API.Core.Services;

/// <summary>
/// Фабрика токенов для почты
/// </summary>
public class TokenFactory : ITokenFactory
{
    /// <inheritdoc />
    public string GetToken() => Random.Shared.Next(1_000_000, 3_000_000).ToString();
}