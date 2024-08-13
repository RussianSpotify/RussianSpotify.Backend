using RussianSpotify.API.Core.Abstractions;

namespace RussianSpotify.API.Core.Entities;

/// <summary>
/// Файл
/// </summary>
public class File : BaseEntity, ISoftDeletable, ITimeTrackable
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="fileName">Назавние файла</param>
    /// <param name="contentType">Тип файла</param>
    /// <param name="address">Адрес в S3</param>
    /// <param name="size">Размер</param>
    /// <param name="user">Пользователь, который загрузил файл</param>
    public File(
        string fileName,
        string contentType,
        string address,
        long size,
        User user)
    {
        FileName = fileName;
        ContentType = contentType;
        Address = address;
        Size = size;
        User = user;
    }

    public File()
    {
    }

    /// <summary>
    /// Адрес на песню в cloud
    /// </summary>
    public string Address { get; protected set; } = default!;

    /// <summary>
    /// Размер файла
    /// </summary>
    public long Size { get; protected set; }

    /// <summary>
    /// Название файла
    /// </summary>
    public string? FileName { get; protected set; }

    /// <summary>
    /// Тип файла
    /// </summary>
    public string? ContentType { get; protected set; }

    /// <summary>
    /// Песня
    /// </summary>
    public Song? Song { get; set; }

    /// <summary>
    /// Плейлист
    /// </summary>
    public Playlist? Playlist { get; set; }

    /// <summary>
    /// Id пользователя, который загрузил файл
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Пользователь, который загрузил файл
    /// </summary>
    public User? User { get; set; }
    
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
    /// <param name="address">Адрес</param>
    /// <param name="size">Размер</param>
    /// <param name="fileName">Название файла</param>
    /// <param name="contentType">Тип</param>
    /// <param name="song">Песня</param>
    /// <param name="playlist">Плейлист (Альбом)</param>
    /// <param name="user">Пользователь</param>
    /// <returns>Тестовая сущность</returns>
    [Obsolete("Только для тестов")]
    public static File CreateForTest(
        Guid id = default,
        string? address = "address/",
        long? size = 200,
        string? fileName = "tester.txt",
        string? contentType = "txt",
        Song? song = default,
        Playlist? playlist = default,
        User? user = default)
        => new()
        {
            Id = id,
            Address = address ?? string.Empty,
            Size = size ?? 0,
            FileName = fileName,
            ContentType = contentType,
            Song = song,
            Playlist = playlist,
            UserId = user?.Id ?? default,
            User = user
        };
}