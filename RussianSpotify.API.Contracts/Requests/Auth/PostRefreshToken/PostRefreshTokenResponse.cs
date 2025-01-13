namespace RussianSpotify.Contracts.Requests.Auth.PostRefreshToken;

/// <summary>
///     Ответ на обновление токена
/// </summary>
public class PostRefreshTokenResponse
{
    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="refreshToken"></param>
    public PostRefreshTokenResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    /// <summary>
    ///     Токен доступа
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    ///     Токен обновления
    /// </summary>
    public string RefreshToken { get; set; }
}