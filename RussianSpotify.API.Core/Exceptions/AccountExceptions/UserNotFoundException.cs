using System.Net;
using RussianSpotify.API.Shared.Exceptions;

namespace RussianSpotify.API.Core.Exceptions.AccountExceptions;

/// <summary>
/// Если user не найден
/// </summary>
public class UserNotFoundException : ApplicationBaseException
{
    public UserNotFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)
    {
    }

    public UserNotFoundException()
    {
    }
}