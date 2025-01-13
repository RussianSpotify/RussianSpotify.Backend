using Newtonsoft.Json;
using RussianSpotify.API.Shared.Domain.Abstractions;

namespace RussianSpotify.API.Files.Domain.Entities;

/// <summary>
/// Файл
/// </summary>
public class FileMetadata : BaseEntity, ISoftDeletable, ITimeTrackable
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">ИД пользователя</param>
    /// <param name="fileName">Название файла</param>
    /// <param name="contentType">Тип файла</param>
    /// <param name="address">Адрес в S3</param>
    /// <param name="size">Размер</param>
    public FileMetadata(
        Guid userId,
        string fileName,
        string contentType,
        string address,
        long size)
    {
        UserId = userId;
        FileName = fileName;
        ContentType = contentType;
        Address = address;
        Size = size;
    }

    /// <summary>
    /// Пустой конструктор
    /// </summary>
    public FileMetadata()
    {
    }

    /// <summary>
    /// Адрес на файл в S3
    /// </summary>
    [JsonProperty(nameof(Address))]
    public string Address { get; protected set; } = default!;

    /// <summary>
    /// Размер файла
    /// </summary>
    [JsonProperty(nameof(Size))]
    public long Size { get; protected set; }

    /// <summary>
    /// Название файла
    /// </summary>
    [JsonProperty(nameof(FileName))]
    public string? FileName { get; protected set; }

    /// <summary>
    /// Тип файла
    /// </summary>
    [JsonProperty(nameof(ContentType))]
    public string? ContentType { get; protected set; }

    /// <summary>
    /// Id пользователя, который загрузил файл
    /// </summary>
    public Guid UserId { get; set; }

    /// <inheritdoc />
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTime? UpdatedAt { get; set; }
    
    /// <inheritdoc />
    public bool IsDeleted { get; set; }

    /// <inheritdoc />
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Создать тестовую сущность
    /// </summary>
    /// <param name="id">ИД</param>
    /// <param name="userId">ИД пользователя</param>
    /// <param name="address">Адрес</param>
    /// <param name="size">Размер</param>
    /// <param name="fileName">Название файла</param>
    /// <param name="contentType">Тип</param>
    /// <returns>Тестовая сущность</returns>
    [Obsolete("Только для тестов")]
    public static FileMetadata CreateForTest(
        Guid id = default,
        Guid userId = default,
        string? address = "address/",
        long? size = 200,
        string? fileName = "tester.txt",
        string? contentType = "txt")
        => new()
        {
            Id = id,
            Address = address ?? string.Empty,
            Size = size ?? 0,
            FileName = fileName,
            ContentType = contentType,
            UserId = userId,
        };
}