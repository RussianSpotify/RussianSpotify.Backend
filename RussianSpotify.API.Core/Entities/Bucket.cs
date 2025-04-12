#region

using RussianSpotify.API.Shared.Domain.Abstractions;
using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Entities;

/// <summary>
///     Корзина пользователя
/// </summary>
public class Bucket : BaseEntity, ISoftDeletable, ITimeTrackable
{
    /// <summary>
    ///     Пользователь
    /// </summary>
    public User User { get; set; }

    /// <summary>
    ///     Ид пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     Песни
    /// </summary>
    public List<Song> Songs { get; set; } = new();

    /// <inheritdoc />
    public bool IsDeleted { get; set; }

    /// <inheritdoc />
    public DateTime? DeletedAt { get; set; }

    /// <inheritdoc />
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    ///     Добавить песню
    /// </summary>
    /// <param name="song">Песню</param>
    public void AddSong(Song song)
    {
        if (Songs is null)
            throw new NotIncludedException(nameof(Songs));

        Songs.Add(song);
    }
}