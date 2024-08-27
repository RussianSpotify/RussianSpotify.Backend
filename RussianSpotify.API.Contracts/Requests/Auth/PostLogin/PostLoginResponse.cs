namespace RussianSpotify.Contracts.Requests.Auth.PostLogin;

/// <summary>
/// Результат логина для PostLogin
/// </summary>
public class PostLoginResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="accessToken">Токен доступа</param>
    /// <param name="refreshToken">Токен обновления</param>
    public PostLoginResponse(string accessToken, string refreshToken)
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