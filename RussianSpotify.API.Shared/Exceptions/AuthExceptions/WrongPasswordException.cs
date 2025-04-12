#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions.AuthExceptions;

/// <summary>
///     Исключение выбрасывается при логине пользователя, если он ввёл неверный пароль от учётной записи
/// </summary>
public class WrongPasswordException : ApplicationBaseException
{
    public WrongPasswordException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)
    {
    }

    public WrongPasswordException()
    {
    }
}