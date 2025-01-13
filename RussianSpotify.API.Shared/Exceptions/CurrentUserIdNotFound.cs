#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions;

/// <summary>
///     Если CurrentUserId из IUserContext равно null
/// </summary>
public class CurrentUserIdNotFound : ApplicationBaseException
{
    public CurrentUserIdNotFound(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)
    {
    }

    public CurrentUserIdNotFound()
    {
    }
}