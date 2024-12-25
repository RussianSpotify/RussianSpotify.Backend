using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Shared.Domain.Abstractions;
using RussianSpotify.Contracts.Enums;

namespace RussianSpotify.API.Core.Entities;

/// <summary>
/// Категория
/// </summary>
public class Category : BaseEntity, ISoftDeletable, ITimeTrackable
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public Category()
    {
        Songs = new List<Song>();
    }

    /// <summary>
    /// Имя категории
    /// </summary>
    public CategoryType CategoryName { get; set; }
    
    /// <inheritdoc />
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTime? UpdatedAt { get; set; }
    
    /// <inheritdoc />
    public bool IsDeleted { get; set; }

    /// <inheritdoc />
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Песни
    /// </summary>
    public List<Song> Songs { get; protected set; }

    /// <summary>
    /// Создать тестовую сущность
    /// </summary>
    /// <param name="id">ИД</param>
    /// <param name="categoryType">Тип категории</param>
    /// <returns>Тестовая сущность</returns>
    public static Category CreateForTest(
        Guid id = default,
        CategoryType categoryType = default)
        => new()
        {
            Id = id,
            CategoryName = categoryType,
        };
}