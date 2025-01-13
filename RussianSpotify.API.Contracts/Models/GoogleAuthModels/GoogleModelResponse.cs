#region

using System.Text.Json.Serialization;

#endregion

namespace RussianSpotify.Contracts.Models.GoogleAuthModels;

/// <summary>
///     Ответ Google авторизации
/// </summary>
public class GoogleModelResponse
{
    /// <summary>
    ///     Токен доступа
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    /// <summary>
    ///     Время жизни токена
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    /// <summary>
    ///     Токен обновления
    /// </summary>
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }

    /// <summary>
    ///     Скоуп
    /// </summary>
    [JsonPropertyName("scope")]
    public string Scope { get; set; }

    /// <summary>
    ///     Тип токена
    /// </summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    /// <summary>
    ///     ИД токена
    /// </summary>
    [JsonPropertyName("id_token")]
    public string IdToken { get; set; }
}