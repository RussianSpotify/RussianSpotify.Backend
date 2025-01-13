#region

using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions.Playlist;

public class PlaylistFileException : BadRequestException
{
    public PlaylistFileException(string message) : base(message)
    {
    }
}