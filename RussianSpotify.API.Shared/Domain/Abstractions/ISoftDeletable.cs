namespace RussianSpotify.API.Shared.Domain.Abstractions;

/// <summary>
/// Интерфейс мягкого удаления
/// </summary>
public interface ISoftDeletable
{
    /// <summary>
    /// Удален
    /// </summary>
    bool IsDeleted { get; set; }

    /// <summary>
    /// Время удаления
    /// </summary>
    public DateTime? DeletedAt { get; set; }
}