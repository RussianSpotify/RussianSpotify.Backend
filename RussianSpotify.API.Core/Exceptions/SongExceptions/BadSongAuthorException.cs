#region

using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions.SongExceptions;

public class BadSongAuthorException : BadRequestException
{
    public BadSongAuthorException(string message) : base(message)
    {
    }
}