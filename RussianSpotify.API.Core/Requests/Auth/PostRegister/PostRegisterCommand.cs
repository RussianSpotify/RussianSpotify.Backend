using MediatR;
using RussianSpotify.Contracts.Requests.Auth.PostRegister;

namespace RussianSpotify.API.Core.Requests.Auth.PostRegister;

/// <summary>
/// Команда на регистрацию
/// </summary>
public class PostRegisterCommand : PostRegisterRequest, IRequest<PostRegisterResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PostRegisterCommand(PostRegisterRequest request)
        : base(request)
    {
    }
}