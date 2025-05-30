#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions.OAuthExceptions;

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