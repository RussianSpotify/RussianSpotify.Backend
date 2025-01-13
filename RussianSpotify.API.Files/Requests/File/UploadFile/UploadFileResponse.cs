namespace RussianSpotify.API.Files.Requests.File.UploadFile;

/// <summary>
///     Ответ на загрузку файла
/// </summary>
public class UploadFileResponse
{
    /// <summary>
    ///     Конструктор, инициализирующий экземпляр класса <see cref="UploadFileResponse" /> с пустым списком файлов.
    /// </summary>
    public UploadFileResponse()
        => FileNameToIds = new List<UploadFileResponseItem>();

    /// <summary>
    ///     Конструктор, инициализирующий экземпляр класса <see cref="UploadFileResponse" /> с переданным списком загруженных
    ///     файлов.
    /// </summary>
    /// <param name="files">Коллекция объектов типа <see cref="UploadFileResponseItem" />, представляющих загруженные файлы.</param>
    public UploadFileResponse(IEnumerable<UploadFileResponseItem> files)
        => FileNameToIds = files.ToList();

    /// <summary>
    ///     Список файлов, представленных объектами <see cref="UploadFileResponseItem" /> с соответствующими ID.
    /// </summary>
    public List<UploadFileResponseItem> FileNameToIds { get; set; }
}