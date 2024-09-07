using Microsoft.Extensions.Configuration;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.Contracts.Models.GoogleAuthModels;

namespace RussianSpotify.API.Core.Services;

/// <summary>
/// Сервис для взаимодействия с Google
/// </summary>
public class GoogleService : IGoogleService
{
    private readonly IGoogleClient _googleClient;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="googleClient">Клиент Google</param>
    /// <param name="configuration">Конфигурация проекта</param>
    public GoogleService(IGoogleClient googleClient, IConfiguration configuration)
    {
        _googleClient = googleClient;
        _configuration = configuration;
    }

    /// <inheritdoc/>
    public async Task<GoogleModelResponse> ExchangeCodeForTokenAsync(string code)
    {
        var data = await GetModelAsync(client => client.GetTokenByCodeAsync(new GoogleModelRequest
        {
            Code = code,
            ClientId = _configuration["Authentication:Google:ClientId"] ?? string.Empty,
            ClientSecret = _configuration["Authentication:Google:ClientSecret"] ?? string.Empty,
            RedirectUri = _configuration["Authentication:Google:RedirectUrl"] ?? string.Empty,
            GrantType = "authorization_code",
        }));

        return data;
    }

    /// <inheritdoc/>
    public async Task<GoogleUserInfoResponse> GetUserInfoAsync(string accessToken)
        => await _googleClient.GetUserInfoAsync(accessToken);

    private async Task<TModel> GetModelAsync<TModel>(Func<IGoogleClient, Task<TModel>> func)
    {
        ArgumentNullException.ThrowIfNull(func);

        try
        {
            return await func(_googleClient)
                   ?? throw new ApplicationBaseException("Не удалось получить ответ от Google");
        }
        catch (Exception e)
        {
            throw new ApplicationBaseException($"Не удалось получить ответ от Google {e.Message}");
        }
    }
}