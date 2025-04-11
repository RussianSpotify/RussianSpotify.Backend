namespace RussianSpotify.API.Shared.Interfaces;

/// <summary>
///     Провайдер даты
/// </summary>
public interface IDateTimeProvider
{
    DateTime CurrentDate { get; }
}