using RussianSpotify.API.Shared.Exceptions;

namespace RussianSpotify.API.Files.Exceptions.FileExceptions;

/// <summary>
/// Исключение, выбрасываемое в случае внутренней ошибки, связанной с файлом.
/// </summary>
public class FileInternalException : InternalException
{
    /// <summary>
    /// Конструктор для создания исключения с сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее внутреннюю ошибку в обработке файла.</param>
    public FileInternalException(string message) : base(message)
    {
    }
}