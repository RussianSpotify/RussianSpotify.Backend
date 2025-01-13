#region

using MediatR;
using RussianSpotify.Contracts.Requests.Auth.PostResetPassword;

#endregion

namespace RussianSpotify.API.Core.Requests.Auth.PostResetPassword;

/// <summary>
///     Команда для сброса пароля
/// </summary>
public class PostResetPasswordCommand : PostResetPasswordRequest, IRequest<PostResetPasswordResponse>
{
    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PostResetPasswordCommand(PostResetPasswordRequest request)
        : base(request)
    {
    }
}