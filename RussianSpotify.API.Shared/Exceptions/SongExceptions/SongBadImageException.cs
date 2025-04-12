#region

#endregion

namespace RussianSpotify.API.Shared.Exceptions.SongExceptions;

public class SongBadImageException : BadRequestException
{
    public SongBadImageException(string message) : base(message)
    {
    }
}