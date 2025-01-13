#region

using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions.Playlist;

public class PlaylistBadImageException : BadRequestException
{
    public PlaylistBadImageException(string message) : base(message)
    {
    }
}