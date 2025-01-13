#region

using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions.SongExceptions;

public class SongBadCategoryException : BadRequestException
{
    public SongBadCategoryException(string message) : base(message)
    {
    }
}