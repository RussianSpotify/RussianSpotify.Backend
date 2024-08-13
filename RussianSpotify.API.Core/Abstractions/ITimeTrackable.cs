namespace RussianSpotify.API.Core.Abstractions;

/// <summary>
/// Интерфейс отслеживания создание/обновление
/// </summary>
public interface ITimeTrackable
{
    /// <summary>
    /// Дата создания
    /// </summary>
    DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Дата обновления
    /// </summary>
    DateTime? UpdatedAt { get; set; }
}