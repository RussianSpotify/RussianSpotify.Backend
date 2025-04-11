#region

using RussianSpotify.API.Shared.Interfaces;

#endregion

namespace RussianSpotify.API.Shared.Services;

/// <summary>
///     Провайдер дат
/// </summary>
public class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc />
    public DateTime CurrentDate => DateTime.UtcNow;
}