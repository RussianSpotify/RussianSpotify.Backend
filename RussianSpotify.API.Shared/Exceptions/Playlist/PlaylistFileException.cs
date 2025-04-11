#region

#endregion

namespace RussianSpotify.API.Shared.Exceptions.Playlist;

public class PlaylistFileException : BadRequestException
{
    public PlaylistFileException(string message) : base(message)
    {
    }
}