#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions.AuthExceptions;

/// <summary>
///     Ошибка выбрасывается, если UserManager не смог по каким-то причинам зарегистрировать пользователя
/// </summary>
public class RegisterUserException : ApplicationBaseException
{
    public RegisterUserException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message, statusCode)
    {
    }

    public RegisterUserException()
    {
    }
}