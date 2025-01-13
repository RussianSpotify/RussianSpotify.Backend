#region

using MediatR;
using RussianSpotify.Contracts.Requests.Auth.PostConfirmEmail;

#endregion

namespace RussianSpotify.API.Core.Requests.Auth.PostConfirmEmail;

/// <summary>
///     Команда для подтверждения почты пользователя
/// </summary>
public class PostConfirmEmailCommand : PostConfirmEmailRequest, IRequest
{
    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PostConfirmEmailCommand(PostConfirmEmailRequest request)
        : base(request)
    {
    }
}