namespace RussianSpotify.API.Shared.Exceptions.SongExceptions;

public class SongNotFoundException : NotFoundException
{
    public SongNotFoundException(string message) : base(message)
    {
    }
}