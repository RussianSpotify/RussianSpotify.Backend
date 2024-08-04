using RussianSpotify.Contracts.Enums;

namespace RussianSpotify.API.Core.Entities;

/// <summary>
/// Категория
/// </summary>
public class Category
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public Category()
    {
        Songs = new List<Song>();
    }

    /// <summary>
    /// Ид категории
    /// </summary>
    public Guid Id { get; protected set; }

    /// <summary>
    /// Имя категории
    /// </summary>
    public CategoryType CategoryName { get; set; }

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