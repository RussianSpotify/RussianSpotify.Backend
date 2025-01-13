#region

using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions.SubscriptionExceptions;

public class SubscriptionBadRequestException : BadRequestException
{
    public SubscriptionBadRequestException(string message) : base(message)
    {
    }
}