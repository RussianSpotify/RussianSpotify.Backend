using MediatR;
using RussianSpotify.Contracts.Requests.Auth.PostRefreshToken;

namespace RussianSpotify.API.Core.Requests.Auth.PostRefreshToken;

/// <summary>
/// Команда для обновления JWT токена
/// </summary>
public class PostRefreshTokenCommand : PostRefreshTokenRequest, IRequest<PostRefreshTokenResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PostRefreshTokenCommand(PostRefreshTokenRequest request)
        : base(request)
    {
    }
}