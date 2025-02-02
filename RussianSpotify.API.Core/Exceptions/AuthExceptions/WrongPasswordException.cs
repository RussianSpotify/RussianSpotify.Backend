#region

using System.Net;
using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions.AuthExceptions;

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