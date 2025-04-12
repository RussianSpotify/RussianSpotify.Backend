#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions.AuthExceptions;

/// <summary>
///     Исключение выбрасывается, если пользователь пытается зарегистрироваться с почтой, которая уже есть в бд
/// </summary>
public class EmailAlreadyRegisteredException : ApplicationBaseException
{
    public EmailAlreadyRegisteredException(string message,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message, statusCode)
    {
    }

    public EmailAlreadyRegisteredException()
    {
    }
}