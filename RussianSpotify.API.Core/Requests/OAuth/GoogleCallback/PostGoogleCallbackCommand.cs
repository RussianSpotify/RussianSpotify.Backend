#region

using MediatR;
using RussianSpotify.Contracts.Requests.OAuth;

#endregion

namespace RussianSpotify.API.Core.Requests.OAuth.GoogleCallback;

/// <summary>
///     Команда на вход через сервис Google
/// </summary>
public class PostGoogleCallbackCommand : IRequest<GetExternalLoginCallbackResponseBase>
{
    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="code">Код</param>
    public PostGoogleCallbackCommand(string code)
        => Code = code;

    /// <summary>
    ///     Код
    /// </summary>
    public string Code { get; }
}