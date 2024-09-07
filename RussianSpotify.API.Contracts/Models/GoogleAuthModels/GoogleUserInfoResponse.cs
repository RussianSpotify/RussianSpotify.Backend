using System.Text.Json.Serialization;

namespace RussianSpotify.Contracts.Models.GoogleAuthModels;

/// <summary>
/// Ответ от сервиса Google о получении данных о пользователе
/// </summary>
public class GoogleUserInfoResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    /// <summary>
    /// Почта пользователя
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    
    /// <summary>
    /// Подтверждена ли почта
    /// </summary>
    [JsonPropertyName("verified_email")]
    public bool VerifiedEmail { get; set; }
    
    /// <summary>
    /// Полное имя (Имя Фамилия)
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    [JsonPropertyName("given_name")]
    public string? GivenName { get; set; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    [JsonPropertyName("family_name")]
    public string? FamilyName { get; set; }
    
    /// <summary>
    /// Фото
    /// </summary>
    [JsonPropertyName("picture")]
    public string? Picture { get; set; }
    
    /// <summary>
    /// Локализация (en, ru, ...)
    /// </summary>
    [JsonPropertyName("locale")]
    public string? Locale { get; set; }
    
    /// <summary>
    /// Адрес компании ex: company.com
    /// </summary>
    [JsonPropertyName("hd")]
    public string Hd { get; set; }
}