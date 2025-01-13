#region

using System.Net;
using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions;

public class ConflictException : ApplicationBaseException
{
    public ConflictException(string message) : base(message, HttpStatusCode.Conflict)
    {
    }
}