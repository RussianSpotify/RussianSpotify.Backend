#region

using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions.SongExceptions;

public class SongInternalException : InternalException
{
    public SongInternalException(string message) : base(message)
    {
    }
}