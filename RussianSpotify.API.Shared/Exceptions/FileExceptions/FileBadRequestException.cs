namespace RussianSpotify.API.Shared.Exceptions.FileExceptions;

public class FileBadRequestException : BadRequestException
{
    public FileBadRequestException(string message) : base(message)
    {
    }
}