#region

using RussianSpotify.API.Client;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.Contracts.Models.GoogleAuthModels;
using static System.String;

#endregion

namespace RussianSpotify.API.Core.Services;

/// <summary>
///     Клиент для работы с Google сервисом
/// </summary>
public class GoogleClient : HttpClientBase, IGoogleClient
{
    /// <inheritdoc />
    public GoogleClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<GoogleModelResponse> GetTokenByCodeAsync(GoogleModelRequest request)
        => await PostAsync<GoogleModelResponse>(Empty, request);

    /// <inheritdoc />
    public async Task<GoogleUserInfoResponse> GetUserInfoAsync(string accessToken)
        => await GetAsync<GoogleUserInfoResponse>(
            $"https://www.googleapis.com/oauth2/v1/userinfo?access_token={accessToken}");
}