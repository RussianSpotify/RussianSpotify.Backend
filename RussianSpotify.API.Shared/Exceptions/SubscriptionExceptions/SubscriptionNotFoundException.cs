namespace RussianSpotify.API.Shared.Exceptions.SubscriptionExceptions;

public class SubscriptionNotFoundException : NotFoundException
{
    public SubscriptionNotFoundException(string message) : base(message)
    {
    }
}