#region

using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions.SubscriptionExceptions;

public class SubscriptionInternalException : InternalException
{
    public SubscriptionInternalException(string message) : base(message)
    {
    }
}