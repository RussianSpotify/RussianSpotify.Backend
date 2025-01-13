#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions;

public class InternalException : ApplicationBaseException
{
    public InternalException(string message) : base(message, HttpStatusCode.InternalServerError)
    {
    }
}