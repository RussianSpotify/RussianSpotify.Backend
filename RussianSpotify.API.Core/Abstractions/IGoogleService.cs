#region

using RussianSpotify.Contracts.Models.GoogleAuthModels;

#endregion

namespace RussianSpotify.API.Core.Abstractions;

/// <summary>
///     Сервис для взаимодействия с Google сервисом
/// </summary>
public interface IGoogleService
{
    /// <summary>
    ///     Получить данные от Google по токену
    /// </summary>
    /// <param name="code">Токен</param>
    public Task<GoogleModelResponse> ExchangeCodeForTokenAsync(string code);

    /// <summary>
    ///     Получить доп.информацию о пользователе
    /// </summary>
    /// <param name="accessToken">Токен доступа, полученный от Google</param>
    /// <returns>Информация пользователя</returns>
    public Task<GoogleUserInfoResponse> GetUserInfoAsync(string accessToken);
}