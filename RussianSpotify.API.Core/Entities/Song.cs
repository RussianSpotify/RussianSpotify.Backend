using RussianSpotify.API.Core.Abstractions;

namespace RussianSpotify.API.Core.Entities;

/// <summary>
/// Песня
/// </summary>
public class Song : BaseEntity, ISoftDeletable, ITimeTrackable
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="songName">Название</param>
    /// <param name="duration">Длительность</param>
    /// <param name="category">Категория</param>
    public Song(string songName, double duration, Category category)
    {
        SongName = songName;
        Duration = duration;
        Category = category;
    }
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public Song()
    {
    }

    /// <summary>
    /// Имя песни
    /// </summary>
    public string SongName { get; set; }

    /// <summary>
    /// Длительность
    /// </summary>
    public double Duration { get; set; }

    /// <summary>
    /// Количество прослушиваний
    /// </summary>
    public uint PlaysNumber { get; set; }

    /// <summary>
    /// Ид категории
    /// </summary>
    public Guid CategoryId { get; protected set; }

    /// <summary>
    /// Nav-prop категории
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// Картинка
    /// </summary>
    public File? Image { get; set; }

    /// <summary>
    /// ИД файла картинки
    /// </summary>
    public Guid? ImageId { get; protected set; }

    /// <summary>
    /// Корзины
    /// </summary>
    public List<Bucket> Buckets { get; protected set; } = new();
    
    /// <summary>
    /// Плейлисты, которым принадлежит песни
    /// </summary>
    public List<Playlist> Playlists { get; protected set; } = new();
    
    /// <summary>
    /// Файлы (тут музыка и картинка)
    /// </summary>
    public List<File> Files { get; set; } = new();

    /// <summary>
    /// Авторы
    /// </summary>
    public List<User> Authors { get; protected set; } = new();
    
    /// <inheritdoc />
    public bool IsDeleted { get; set; }

    /// <inheritdoc />
    public DateTime? DeletedAt { get; set; }
    
    /// <inheritdoc />
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Создать тестовую сущность
    /// </summary>
    /// <param name="id">ИД</param>
    /// <param name="songName">Название</param>
    /// <param name="duration">Длительность</param>
    /// <param name="playsNumber">Кол-во прослушиваний</param>
    /// <param name="category">Категория</param>
    /// <param name="image">Фото</param>
    /// <param name="authors">Авторы</param>
    /// <param name="files">Файлы (тут музыка и картинка)</param>
    /// <returns>Тестовая сущность</returns>
    [Obsolete("Только для тестов")]
    public static Song CreateForTest(
        Guid id = default,
        string? songName = default,
        double? duration = default,
        uint? playsNumber = default,
        Category? category = default,
        File? image = default,
        List<User>? authors = default,
        List<File>? files = default)
        => new()
        {
            Id = id,
            SongName = songName ?? string.Empty,
            Duration = duration ?? 0,
            PlaysNumber = playsNumber ?? 0,
            CategoryId = default,
            Category = category ?? new Category(),
            Image = image,
            Authors = authors ?? new(),
            Files = files ?? new(),
        };
    
    /// <summary>
    /// Добавить автора
    /// </summary>
    /// <param name="author">Автор</param>
    public void AddAuthor(User author)
    {
        Authors.Add(author);
    }

    /// <summary>
    /// Удалить автора
    /// </summary>
    /// <param name="author">Автор</param>
    public void RemoveAuthor(User author)
    {
        Authors.Remove(author);
    }
}