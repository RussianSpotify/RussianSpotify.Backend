#region

#endregion

namespace RussianSpotify.API.Shared.Exceptions.SubscriptionExceptions;

public class WrongUserException : BadRequestException
{
    public WrongUserException(string message) : base(message)
    {
    }
}