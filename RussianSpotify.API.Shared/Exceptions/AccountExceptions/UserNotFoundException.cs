#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions.AccountExceptions;

/// <summary>
///     Если user не найден
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