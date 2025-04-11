#region

#endregion

namespace RussianSpotify.API.Shared.Exceptions.SongExceptions;

public class SongBadCategoryException : BadRequestException
{
    public SongBadCategoryException(string message) : base(message)
    {
    }
}