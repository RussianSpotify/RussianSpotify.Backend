using RussianSpotify.Contracts.Models.GoogleAuthModels;

namespace RussianSpotify.API.Core.Abstractions;

/// <summary>
/// Клиент для отправки запросов в Google
/// </summary>
public interface IGoogleClient
{
    /// <summary>
    /// Получить токены с Google Service
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <returns>Данные с токеном</returns>
    public Task<GoogleModelResponse> GetTokenByCodeAsync(GoogleModelRequest request);

    /// <summary>
    /// Получить данные о пользователе
    /// </summary>
    /// <param name="accessToken">Токен доступа, полученный от Google</param>
    /// <returns></returns>
    public Task<GoogleUserInfoResponse> GetUserInfoAsync(string accessToken);
}