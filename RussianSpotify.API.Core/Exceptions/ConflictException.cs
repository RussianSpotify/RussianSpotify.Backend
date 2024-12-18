using System.Net;
using RussianSpotify.API.Shared.Exceptions;

namespace RussianSpotify.API.Core.Exceptions;

public class ConflictException : ApplicationBaseException
{
    public ConflictException(string message) : base(message, HttpStatusCode.Conflict)
    {
    }
}