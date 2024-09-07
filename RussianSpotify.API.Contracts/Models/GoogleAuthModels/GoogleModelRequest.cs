using System.Text.Json.Serialization;

namespace RussianSpotify.Contracts.Models.GoogleAuthModels;

/// <summary>
/// Данные для отправки в сервис Google
/// </summary>
public class GoogleModelRequest
{
    /// <summary>
    /// Код, который пришел с фронта
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; set; }

    /// <summary>
    /// Идентификатор приложения
    /// </summary>
    [JsonPropertyName("client_id")]
    public string ClientId { get; set; }

    /// <summary>
    /// Ключ приложения
    /// </summary>
    [JsonPropertyName("client_secret")]
    public string ClientSecret { get; set; }

    /// <summary>
    /// Обратный путь
    /// </summary>
    [JsonPropertyName("redirect_uri")]
    public string RedirectUri { get; set; }

    /// <summary>
    /// Тип доверености
    /// </summary>
    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; }
}