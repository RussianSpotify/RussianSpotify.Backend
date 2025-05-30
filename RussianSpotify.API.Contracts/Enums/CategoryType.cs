#region

using System.ComponentModel;

#endregion

namespace RussianSpotify.Contracts.Enums;

/// <summary>
///     Категории
/// </summary>
public enum CategoryType
{
    [Description("Реп")] Rap = 1,

    [Description("Хип-хоп")] HipHop = 2,

    [Description("Металл")] Metall = 3,

    [Description("Рок")] Rock = 4,
}