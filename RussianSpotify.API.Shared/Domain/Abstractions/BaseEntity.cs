namespace RussianSpotify.API.Shared.Domain.Abstractions;

/// <summary>
///     Базовая сущность
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public Guid Id { get; set; }
}