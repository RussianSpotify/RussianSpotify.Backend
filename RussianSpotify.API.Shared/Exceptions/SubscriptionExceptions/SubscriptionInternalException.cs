#region

#endregion

namespace RussianSpotify.API.Shared.Exceptions.SubscriptionExceptions;

public class SubscriptionInternalException : InternalException
{
    public SubscriptionInternalException(string message) : base(message)
    {
    }
}