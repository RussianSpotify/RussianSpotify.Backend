#region

using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions.SongExceptions;

public class SongBadFileException : BadRequestException
{
    public SongBadFileException(string message) : base(message)
    {
    }
}