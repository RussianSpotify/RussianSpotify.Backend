#region

using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using RussianSpotify.Contracts.Models;
using JsonException = System.Text.Json.JsonException;
using JsonSerializer = System.Text.Json.JsonSerializer;

#endregion

namespace RussianSpotify.API.Client;

/// <summary>
///     Базовый Http Client
/// </summary>
public class HttpClientBase
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="httpClient">Http клиент</param>
    public HttpClientBase(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = InitSerializationOptions();
    }

    /// <summary>
    ///     POST
    /// </summary>
    /// <typeparam name="TResponse">Тип ответа</typeparam>
    /// <param name="url">url</param>
    /// <param name="data">Тело</param>
    /// <returns>Ответ</returns>
    protected async Task<TResponse> PostAsync<TResponse>(string url, object data)
    {
        var responseMessage = await _httpClient.PostAsync(url, GetJsonContent(data)).ConfigureAwait(false);

        if (!responseMessage.IsSuccessStatusCode)
            await HandleUnsuccessfullResponseAsync(responseMessage).ConfigureAwait(false);

        return await ExtractJsonDataAsync<TResponse>(responseMessage).ConfigureAwait(false);
    }

    /// <summary>
    ///     GET
    /// </summary>
    /// <typeparam name="TResponse">Тип ответа</typeparam>
    /// <param name="url">url</param>
    /// <param name="data">query string</param>
    /// <param name="accessToken">Токен Bearer</param>
    /// <returns>Ответ</returns>
    protected async Task<TResponse> GetAsync<TResponse>(string url, object? data = null, string? accessToken = null)
        where TResponse : new()
    {
        if (accessToken != null)
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", accessToken);

        var responseMessage = await _httpClient.GetAsync($"{url}").ConfigureAwait(false);

        if (!responseMessage.IsSuccessStatusCode)
            await HandleUnsuccessfullResponseAsync(responseMessage).ConfigureAwait(false);

        return await ExtractJsonDataAsync<TResponse>(responseMessage).ConfigureAwait(false);
    }

    private StringContent GetJsonContent(object data)
        => new(JsonSerializer.Serialize(data, _options), Encoding.UTF8, "application/json");

    private static JsonSerializerOptions InitSerializationOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.Cyrillic, UnicodeRanges.BasicLatin),
        };

        return options;
    }

    /// <summary>
    ///     Обработка исключения для ответа с ошибочным статусом
    /// </summary>
    /// <param name="responseMessage">Ответ сервера</param>
    /// <returns>-</returns>
    private async Task HandleUnsuccessfullResponseAsync(HttpResponseMessage responseMessage)
    {
        try
        {
            var details = await ExtractJsonDataAsync<ProblemDetailsResponse>(responseMessage).ConfigureAwait(false);
            var message = details?.Title ?? details?.Detail ?? "Ошибка при обработке запроса";
            throw new ApplicationException(message);
        }
        catch (JsonException)
        {
            var responseText = await responseMessage.Content.ReadAsStringAsync();
            throw new ApplicationException($"Произошло неожиданное исключение: {responseText}");
        }
    }

    private async Task<TResponse> ExtractJsonDataAsync<TResponse>(HttpResponseMessage responseMessage)
    {
        if (responseMessage?.Content is null)
            return default!;

        var responseStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
        if (responseStream is null)
            return default!;

        return (await JsonSerializer.DeserializeAsync<TResponse>(responseStream, _options).ConfigureAwait(false))!;
    }
}