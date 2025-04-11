#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions.AccountExceptions;

public class InvalidChangePasswordException : ApplicationBaseException
{
    public InvalidChangePasswordException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)
    {
    }

    public InvalidChangePasswordException()
    {
    }
}