#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions.AuthExceptions;

/// <summary>
///     Если пришёл не валидный JWT или Refresh Token
/// </summary>
public class InvalidTokenException : ApplicationBaseException
{
    public InvalidTokenException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)
    {
    }

    public InvalidTokenException()
    {
    }
}