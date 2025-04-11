#region

#endregion

namespace RussianSpotify.API.Shared.Exceptions.Playlist;

public class PlaylistBadRequestException : BadRequestException
{
    public PlaylistBadRequestException(string message) : base(message)
    {
    }
}