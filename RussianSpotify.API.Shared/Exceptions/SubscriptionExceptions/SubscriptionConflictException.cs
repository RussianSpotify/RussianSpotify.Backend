namespace RussianSpotify.API.Shared.Exceptions.SubscriptionExceptions;

public class SubscriptionConflictException : ConflictException
{
    public SubscriptionConflictException(string message) : base(message)
    {
    }
}