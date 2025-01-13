#region

using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions.Playlist;

public class PlaylistBadRequestException : BadRequestException
{
    public PlaylistBadRequestException(string message) : base(message)
    {
    }
}