#region

using MediatR;
using RussianSpotify.Contracts.Requests.Auth.PostRevokeToken;

#endregion

namespace RussianSpotify.API.Core.Requests.Auth.PostRevokeToken;

/// <summary>
///     Команда для обнуления Refresh токена
/// </summary>
public class PostRevokeTokenCommand : PostRevokeTokenRequest, IRequest
{
    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PostRevokeTokenCommand(PostRevokeTokenRequest request)
        : base(request)
    {
    }
}