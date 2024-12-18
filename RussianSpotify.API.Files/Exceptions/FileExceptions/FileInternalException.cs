using RussianSpotify.API.Shared.Exceptions;

namespace RussianSpotify.API.Files.Exceptions.FileExceptions;

public class FileInternalException : InternalException
{
    public FileInternalException(string message) : base(message)
    {
    }
}