#region

using MediatR;
using RussianSpotify.Contracts.Requests.Auth.PostConfirmPasswordReset;

#endregion

namespace RussianSpotify.API.Core.Requests.Auth.PostConfirmPasswordReset;

/// <summary>
///     Команда на подтверждение сброса пароля
/// </summary>
public class PostConfirmPasswordResetCommand : PostConfirmPasswordResetRequest, IRequest
{
    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="request">PostConfirmPasswordResetRequest</param>
    public PostConfirmPasswordResetCommand(PostConfirmPasswordResetRequest request)
        : base(request)
    {
    }
}