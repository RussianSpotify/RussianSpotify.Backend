using RussianSpotify.API.Shared.Exceptions;

namespace RussianSpotify.API.Files.Exceptions.FileExceptions;

/// <summary>
/// Исключение, выбрасываемое в случае неправильного запроса, связанного с файлом.
/// </summary>
public class FileBadRequestException : BadRequestException
{
    /// <summary>
    /// Конструктор для создания исключения с сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку в запросе.</param>
    public FileBadRequestException(string message) : base(message)
    {
    }
}