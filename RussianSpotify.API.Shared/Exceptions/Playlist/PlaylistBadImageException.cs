#region

#endregion

namespace RussianSpotify.API.Shared.Exceptions.Playlist;

public class PlaylistBadImageException : BadRequestException
{
    public PlaylistBadImageException(string message) : base(message)
    {
    }
}