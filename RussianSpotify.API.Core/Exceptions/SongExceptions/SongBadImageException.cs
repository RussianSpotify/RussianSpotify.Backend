#region

using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions.SongExceptions;

public class SongBadImageException : BadRequestException
{
    public SongBadImageException(string message) : base(message)
    {
    }
}