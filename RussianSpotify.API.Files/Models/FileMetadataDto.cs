namespace RussianSpotify.API.Files.Models;

/// <summary>
/// Метаданные файла для сериализации и хранения в Redis
/// </summary>
public class FileMetadataDto
{
    /// <summary>
    /// Название файла
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Размер файла в байтах
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// Тип содержимого файла (MIME-тип)
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// Id пользователя, загрузившего файл
    /// </summary>
    public Guid UploadedBy { get; set; }

    /// <summary>
    /// Дата и время загрузки файла
    /// </summary>
    public DateTime CreatedAt { get; set; }
}