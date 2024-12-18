using System.Net;

namespace RussianSpotify.API.Shared.Exceptions;

public class BadRequestException : ApplicationBaseException
{
    public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}