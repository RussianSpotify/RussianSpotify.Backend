#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions;

public class ConflictException : ApplicationBaseException
{
    public ConflictException(string message) : base(message, HttpStatusCode.Conflict)
    {
    }
}