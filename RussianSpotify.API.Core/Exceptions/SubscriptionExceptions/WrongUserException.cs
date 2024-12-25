using Microsoft.AspNetCore.Mvc;
using RussianSpotify.API.Shared.Exceptions;

namespace RussianSpotify.API.Core.Exceptions.SubscriptionExceptions;

public class WrongUserException : BadRequestException
{
    public WrongUserException(string message) : base(message)
    {
    }
}