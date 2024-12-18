namespace RussianSpotify.API.Shared.Requests.File;

public class UploadFileRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="fileStream">Бинарь</param>
    /// <param name="fileName">Название файла</param>
    /// <param name="contentType">Тип файла</param>
    public UploadFileRequest(Stream fileStream, string fileName, string contentType)
    {
        FileStream = fileStream;
        FileName = fileName;
        ContentType = contentType;
    }

    /// <summary>
    /// Поток
    /// </summary>
    public Stream FileStream { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ContentType { get; set; }
}