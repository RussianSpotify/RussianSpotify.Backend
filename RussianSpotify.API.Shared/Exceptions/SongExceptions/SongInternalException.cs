#region

#endregion

namespace RussianSpotify.API.Shared.Exceptions.SongExceptions;

public class SongInternalException : InternalException
{
    public SongInternalException(string message) : base(message)
    {
    }
}