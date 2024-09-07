namespace RussianSpotify.Contracts.Requests.OAuth;

/// <summary>
/// Ответ на вход через сторонние сервисы
/// </summary>
public class GetExternalLoginCallbackResponseBase
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="accessToken">Токен доступа</param>
    /// <param name="refreshToken">Токен обновления</param>
    public GetExternalLoginCallbackResponseBase(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    /// <summary>
    /// Токен доступа
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// Токен обновления
    /// </summary>
    public string RefreshToken { get; set; }
}