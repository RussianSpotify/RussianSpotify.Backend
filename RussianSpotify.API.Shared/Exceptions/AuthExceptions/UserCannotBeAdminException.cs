#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions.AuthExceptions;

public class UserCannotBeAdminException : ApplicationBaseException
{
    public UserCannotBeAdminException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)
    {
    }

    public UserCannotBeAdminException()
    {
    }
}