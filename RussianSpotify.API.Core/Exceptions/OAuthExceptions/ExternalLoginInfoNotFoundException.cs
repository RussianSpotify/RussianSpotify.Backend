#region

using System.Net;
using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions.OAuthExceptions;

/// <summary>
///     Если после авторизации через сторонний сервис не найдено ExternalLoginInfo
/// </summary>
public class ExternalLoginInfoNotFoundException : ApplicationBaseException
{
    public ExternalLoginInfoNotFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)

    {
    }

    public ExternalLoginInfoNotFoundException()
    {
    }
}