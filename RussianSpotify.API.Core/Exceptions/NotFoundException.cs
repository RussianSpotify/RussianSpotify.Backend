#region

using System.Net;
using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions;

public class NotFoundException : ApplicationBaseException
{
    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
    {
    }
}