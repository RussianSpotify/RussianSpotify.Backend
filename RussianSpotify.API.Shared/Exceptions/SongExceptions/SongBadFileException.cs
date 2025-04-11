#region

#endregion

namespace RussianSpotify.API.Shared.Exceptions.SongExceptions;

public class SongBadFileException : BadRequestException
{
    public SongBadFileException(string message) : base(message)
    {
    }
}