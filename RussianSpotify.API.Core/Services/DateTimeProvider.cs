#region

using RussianSpotify.API.Core.Abstractions;

#endregion

namespace RussianSpotify.API.Core.Services;

/// <summary>
///     Провайдер дат
/// </summary>
public class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc />
    public DateTime CurrentDate => DateTime.UtcNow;
}