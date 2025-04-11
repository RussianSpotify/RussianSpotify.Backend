#region

#endregion

namespace RussianSpotify.API.Shared.Exceptions.SongExceptions;

public class SongBadRequestException : BadRequestException
{
    public SongBadRequestException(string message) : base(message)
    {
    }
}