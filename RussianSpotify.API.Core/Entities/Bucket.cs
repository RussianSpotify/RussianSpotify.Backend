using RussianSpotify.API.Core.Abstractions;

namespace RussianSpotify.API.Core.Entities;

/// <summary>
/// Корзина пользователя
/// </summary>
public class Bucket : BaseEntity, ISoftDeletable, ITimeTrackable
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Ид пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Песни
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
}