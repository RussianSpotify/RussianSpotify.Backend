namespace RussianSpotify.API.Files.Models;

/// <summary>
/// Файл для S3
/// </summary>
public class FileContent
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="content">Бинарные данные</param>
    /// <param name="fileName">Название файла</param>
    /// <param name="contentType">Тип контента</param>
    /// <param name="bucket">Название бакета</param>
    public FileContent(
        Stream content,
        string fileName,
        string contentType,
        string? bucket)
    {
        Content = content;
        FileName = fileName;
        ContentType = contentType;
        Bucket = bucket;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public FileContent()
    {
    }

    /// <summary>
    /// Бинарные данные файла
    /// </summary>
    public Stream Content { get; set; }

    /// <summary>
    /// Название файла
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Тип контента
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// Название бакета
    /// </summary>
    public string? Bucket { get; set; }

    /// <summary>
    /// Размер файла (в байтах)
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// Пользователь, загрузивший файл
    /// </summary>
    public Guid UploadedBy { get; set; } = Guid.Empty;

    /// <summary>
    /// Дата создания файла
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Создать тестовую сущность
    /// </summary>
    /// <param name="content">Бинарные данные файла</param>
    /// <param name="fileName">Название файла</param>
    /// <param name="contentType">Тип контента</param>
    /// <param name="bucket">Название бакета</param>
    /// <param name="fileSize">Размер файла</param>
    /// <param name="uploadedBy">Пользователь, загрузивший файл</param>
    /// <param name="createdAt">Дата создания файла</param>
    /// <returns>Тестовая сущность</returns>
    [Obsolete("Только для тестов")]
    public static FileContent CreateForTest(
        Stream? content = default,
        string fileName = "testFile",
        string contentType = ".mp3",
        string bucket = "testBucket",
        long fileSize = 0,
        Guid uploadedBy = default,
        DateTime createdAt = default)
        => new()
        {
            Content = content ?? new MemoryStream(),
            FileName = fileName,
            ContentType = contentType,
            Bucket = bucket,
            FileSize = fileSize,
            UploadedBy = uploadedBy,
            CreatedAt = createdAt
        };
}