#region

#endregion

namespace RussianSpotify.API.Shared.Exceptions.SubscriptionExceptions;

public class SubscriptionBadRequestException : BadRequestException
{
    public SubscriptionBadRequestException(string message) : base(message)
    {
    }
}