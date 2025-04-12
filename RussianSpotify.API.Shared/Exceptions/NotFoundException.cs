#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions;

public class NotFoundException : ApplicationBaseException
{
    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
    {
    }
}