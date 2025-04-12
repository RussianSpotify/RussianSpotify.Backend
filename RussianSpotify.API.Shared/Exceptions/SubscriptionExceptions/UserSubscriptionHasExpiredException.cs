#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions.SubscriptionExceptions;

public class UserSubscriptionHasExpiredException : ApplicationBaseException
{
    public UserSubscriptionHasExpiredException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) :
        base(message, statusCode)
    {
    }

    public UserSubscriptionHasExpiredException()
    {
    }
}