namespace RussianSpotify.API.Shared.Exceptions.SongExceptions;

public class SongForbiddenException : ForbiddenException
{
    public SongForbiddenException(string message) : base(message)
    {
    }
}