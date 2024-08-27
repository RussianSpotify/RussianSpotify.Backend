namespace RussianSpotify.API.Core.Abstractions;

/// <summary>
/// Фабрика токенов для почты
/// </summary>
public interface ITokenFactory
{
    /// <summary>
    /// Получить рандомный токен
    /// </summary>
    /// <returns>Токен</returns>
    public string GetToken();
}