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
    /// Создать тестовую сущность
    /// </summary>
    /// <param name="content">Бинарные данные файла</param>
    /// <param name="fileName">Название файла</param>
    /// <param name="contentType">Тип контента</param>
    /// <param name="bucket">Название бакета</param>
    /// <returns>Тестовая сущность</returns>
    [Obsolete("Только для тестов")]
    public static FileContent CreateForTest(
        Stream? content = default,
        string fileName = "testFile",
        string contentType = ".mp3",
        string bucket = "testBucket")
        => new()
        {
            Content = content ?? new MemoryStream(),
            FileName = fileName,
            ContentType = contentType,
            Bucket = bucket
        };
}