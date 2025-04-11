#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions.AuthExceptions;

/// <summary>
///     Если у пользователя не подтвержён Email, но он хочет залогиниться
/// </summary>
public class NotConfirmedEmailException : ApplicationBaseException
{
    public NotConfirmedEmailException(string message,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, statusCode)
    {
    }

    public NotConfirmedEmailException()
    {
    }
}