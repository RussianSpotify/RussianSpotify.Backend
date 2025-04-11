namespace RussianSpotify.API.Shared.Exceptions.Playlist;

public class PlaylistForbiddenException : ForbiddenException
{
    public PlaylistForbiddenException(string message) : base(message)
    {
    }
}