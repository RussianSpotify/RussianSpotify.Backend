#region

#endregion

namespace RussianSpotify.API.Shared.Exceptions.SongExceptions;

public class BadSongAuthorException : BadRequestException
{
    public BadSongAuthorException(string message) : base(message)
    {
    }
}