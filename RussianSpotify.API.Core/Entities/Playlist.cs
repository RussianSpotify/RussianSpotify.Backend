using RussianSpotify.API.Core.Abstractions;

namespace RussianSpotify.API.Core.Entities;

/// <summary>
/// Плейлист или альбом
/// </summary>
public class Playlist : BaseEntity, ISoftDeletable, ITimeTrackable
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public Playlist()
    {
        Songs = new List<Song>();
        Users = new List<User>();
    }

    /// <summary>
    /// Название плейлиста
    /// </summary>
    public string PlaylistName { get; set; } = default!;

    /// <summary>
    /// Картинка
    /// </summary>
    public File? Image { get; set; }

    /// <summary>
    /// ИД картинки
    /// </summary>
    public Guid? ImageId { get; set; }

    /// <summary>
    /// Ид автора
    /// </summary>
    public Guid AuthorId { get; protected set; }

    /// <summary>
    /// Nav-prop автора
    /// </summary>
    public User? Author { get; set; }

    /// <summary>
    /// Дата опубликования
    /// </summary>
    public DateTime ReleaseDate { get; set; }

    /// <summary>
    /// Количество прослушиваний плейлиста
    /// </summary>
    public uint PlaysNumber { get; set; }

    /// <summary>
    /// Песни
    /// </summary>
    public List<Song>? Songs { get; set; }

    /// <summary>
    /// Лайкнувшие пользователи
    /// </summary>
    public List<User>? Users { get; set; }

    /// <summary>
    /// Таблица со связями {<see cref="User"/>, <see cref="Playlist"/>}
    /// </summary>
    public List<PlaylistUser> PlaylistUsers { get; set; } = new();

    /// <summary>
    /// Является ли альбомом
    /// </summary>
    public bool IsAlbum { get; set; }
    
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
    /// <param name="playlistName">Название плейлиста</param>
    /// <param name="releaseDate">Дата опубликования</param>
    /// <param name="id">Ид</param>
    /// <param name="image">Фото</param>
    /// <param name="author">Автор</param>
    /// <param name="playsNumber">Кол-во прослушиваний</param>
    /// <param name="isAlbum">Альбом</param>
    /// <param name="songs">Песни</param>
    /// <returns>Тестовая сущность</returns>
    [Obsolete("Только для тестов")]
    public static Playlist CreateForTest(
        string playlistName,
        DateTime releaseDate = default,
        Guid? id = default,
        File? image = default,
        User? author = default,
        uint playsNumber = default,
        bool isAlbum = default,
        List<Song>? songs = default)
        => new()
        {
            Id = id ?? default,
            PlaylistName = playlistName,
            Image = image,
            ImageId = image?.Id,
            AuthorId = author?.Id ?? default,
            Author = author,
            ReleaseDate = releaseDate,
            PlaysNumber = playsNumber,
            IsAlbum = isAlbum,
            Songs = songs,
        };
}