using RussianSpotify.API.Shared.Requests.File;

namespace RussianSpotify.API.Files.Requests.File.UploadFile;

/// <summary>
/// Запрос элемента для <see cref="UploadRequestItem"/>
/// </summary>
public class UploadRequestItem : UploadFileRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="fileStream">Бинарь</param>
    /// <param name="fileName">Название файла</param>
    /// <param name="contentType">Тип файла</param>
    public UploadRequestItem(Stream fileStream, string fileName, string contentType) : base(fileStream, fileName,
        contentType) { }
}