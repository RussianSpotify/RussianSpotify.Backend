#region

using System.Net;
using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions.SubscriptionExceptions;

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