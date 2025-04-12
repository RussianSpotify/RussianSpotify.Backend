#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions.AuthExceptions;

/// <summary>
///     Исключение выбрасывается при логине пользователя, если не удалось найти пользователя с нужной почтой
/// </summary>
public class NotFoundUserException : ApplicationBaseException
{
    public NotFoundUserException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)
    {
    }

    public NotFoundUserException()
    {
    }
}