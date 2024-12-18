using RussianSpotify.API.Shared.Exceptions;

namespace RussianSpotify.API.Files.Exceptions.FileExceptions;

public class FileBadRequestException : BadRequestException
{
    public FileBadRequestException(string message) : base(message)
    {
    }
}