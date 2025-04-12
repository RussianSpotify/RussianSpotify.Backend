#region

using RussianSpotify.API.Shared.Domain.Abstractions;
using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Entities;

/// <summary>
///     Сущность пользователя
/// </summary>
public class User : BaseEntity, ISoftDeletable, ITimeTrackable
{
    private string _userName = default!;
    private string _email = default!;
    private string _passwordHash = default!;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="userName">Имя пользователя</param>
    /// <param name="email">Почта</param>
    /// <param name="passwordHash">Хеш пароля</param>
    /// <param name="isConfirmed">Подтверждена почта</param>
    /// <param name="phone">Телефон</param>
    /// <param name="userPhotoId">ИД файла фотографии</param>
    /// <param name="birthday">День рождения</param>
    /// <param name="bucket">Корзина</param>
    /// <param name="subscribe">Подписка</param>
    /// <param name="playlists">Плейлисты</param>
    /// <param name="authorPlaylists">Плейлисты автора</param>
    /// <param name="songs">Песни</param>
    /// <param name="roles">Роли</param>
    public User(
        string userName,
        string email,
        string passwordHash,
        bool isConfirmed = false,
        string? phone = default,
        Guid? userPhotoId = default,
        DateTime? birthday = default,
        Bucket? bucket = default,
        Subscribe? subscribe = default,
        List<Playlist>? playlists = default,
        List<Playlist>? authorPlaylists = default,
        List<Song>? songs = default,
        List<Role>? roles = default)
    {
        UserName = userName;
        UserPhotoId = userPhotoId;
        Email = email;
        PasswordHash = passwordHash;
        Birthday = birthday;
        Phone = phone;
        IsConfirmed = isConfirmed;
        Bucket = bucket ?? new Bucket();
        Subscribe = subscribe;
        Playlists = playlists ?? new List<Playlist>();
        AuthorPlaylists = authorPlaylists ?? new List<Playlist>();
        Songs = songs ?? new List<Song>();
        Roles = roles ?? new List<Role>();
        Chats = new List<Chat>();
        Messages = new();
    }

    /// <summary>
    ///     Конструктор
    /// </summary>
    private User()
    {
    }

    /// <summary>
    ///     Логин пользователя
    /// </summary>
    public string UserName
    {
        get => _userName;
        set => _userName = string.IsNullOrWhiteSpace(value)
            ? throw new RequiredFieldException("Логин пользователя")
            : value;
    }

    /// <summary>
    ///     Почта пользователя
    /// </summary>
    public string Email
    {
        get => _email;
        set => _email = string.IsNullOrWhiteSpace(value)
            ? throw new RequiredFieldException("Почта пользователя")
            : value;
    }

    /// <summary>
    ///     Хеш пароля
    /// </summary>
    public string PasswordHash
    {
        get => _passwordHash;
        set => _passwordHash = string.IsNullOrWhiteSpace(value)
            ? throw new RequiredFieldException("Хеш пароля")
            : value;
    }

    /// <summary>
    ///     JWT
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    ///     Токен для обновления JWT
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    ///     Время жизни Refresh Token
    /// </summary>
    public DateTime? RefreshTokenExpiryTime { get; set; }

    /// <summary>
    ///     Id фото в профиле юзера
    /// </summary>
    public Guid? UserPhotoId { get; set; }

    /// <summary>
    ///     День рождения пользователя
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    ///     Телефон пользователя
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    ///     Подтвержден
    /// </summary>
    public bool IsConfirmed { get; set; }

    /// <summary>
    ///     Корзина
    /// </summary>
    public Bucket? Bucket { get; set; }

    /// <summary>
    ///     Подписка
    /// </summary>
    public Subscribe? Subscribe { get; set; }

    /// <summary>
    ///     Чаты
    /// </summary>
    public List<Chat> Chats { get; set; }

    /// <summary>
    ///     Сообщения, отправленные пользователем
    /// </summary>
    public List<Message> Messages { get; set; }

    /// <summary>
    ///     Понравившиеся плейлисты
    /// </summary>
    public List<Playlist>? Playlists { get; set; }

    /// <summary>
    ///     Таблица со связями {<see cref="User" />, <see cref="Playlist" />}
    /// </summary>
    public List<PlaylistUser> PlaylistUsers { get; set; } = new();

    /// <summary>
    ///     Плейлисты, созданные этим автором
    /// </summary>
    public List<Playlist> AuthorPlaylists { get; set; }

    /// <summary>
    ///     Песни пользователя
    /// </summary>
    public List<Song>? Songs { get; protected set; }

    /// <summary>
    ///     Роли
    /// </summary>
    public List<Role> Roles { get; set; }

    /// <inheritdoc />
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTime? UpdatedAt { get; set; }

    /// <inheritdoc />
    public bool IsDeleted { get; set; }

    /// <inheritdoc />
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    ///     Создать тестовую сущность
    /// </summary>
    /// <param name="id">Ид пользователя</param>
    /// <param name="login">Логин пользователя</param>
    /// <param name="birthday">Дата рождения</param>
    /// <param name="email">E-mail пользователя</param>
    /// <param name="phone">Телефон</param>
    /// <param name="passwordHash">Хеш пароля</param>
    /// <param name="roles">Роли</param>
    /// <returns></returns>
    [Obsolete("Только для тестов")]
    public static User CreateForTest(
        Guid? id = default,
        string? login = default!,
        DateTime? birthday = default,
        string? email = default!,
        string? phone = default!,
        string? passwordHash = default,
        List<Role>? roles = default)
        => new()
        {
            Id = id ?? Guid.NewGuid(),
            _userName = login ?? string.Empty,
            Birthday = birthday,
            Bucket = new Bucket(),
            _email = email ?? string.Empty,
            Phone = phone,
            _passwordHash = passwordHash ?? string.Empty,
            Roles = roles ?? new List<Role>(),
        };

    /// <summary>
    ///     Добавить роль
    /// </summary>
    /// <param name="role">Роль</param>
    public void AddRole(Role role)
    {
        if (Roles is null)
            throw new NotIncludedException(nameof(Roles));

        Roles.Add(role);
    }
}