namespace RussianSpotify.Contracts.Requests.Account.PatchUpdateUserInfo;

/// <summary>
/// Ответ на запрос на обновление данных о пользователе
/// </summary>
public class PatchUpdateUserInfoResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="accessToken">Токен доступа</param>
    /// <param name="refreshToken">Токен обновления</param>
    public PatchUpdateUserInfoResponse(string accessToken, string refreshToken)
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