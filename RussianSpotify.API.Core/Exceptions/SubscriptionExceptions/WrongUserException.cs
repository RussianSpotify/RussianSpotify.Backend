#region

using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions.SubscriptionExceptions;

public class WrongUserException : BadRequestException
{
    public WrongUserException(string message) : base(message)
    {
    }
}