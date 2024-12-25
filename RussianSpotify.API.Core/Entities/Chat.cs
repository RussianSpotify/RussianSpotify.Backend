using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Shared.Domain.Abstractions;

namespace RussianSpotify.API.Core.Entities;

/// <summary>
/// Чат
/// </summary>
public class Chat : BaseEntity
{
    private string _name = default!;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="name">Название чата</param>
    /// <param name="messages">Сообщения</param>
    /// <param name="users">Пользователи чата</param>
    public Chat(string name, List<User>? users, List<Message>? messages = default)
    {
        Name = name;
        Messages = messages ?? new();
        Users = users ?? new List<User>();
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    private Chat()
    {
    }

    /// <summary>
    /// Название чата
    /// </summary>
    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string Name
    {
        get => _name;
        set => _name = string.IsNullOrEmpty(value)
            ? throw new RequiredFieldException("Название чата")
            : value;
    }

    /// <summary>
    /// Сообщения в чате
    /// </summary>
    public ICollection<Message> Messages { get; set; }

    /// <summary>
    /// Пользователи
    /// </summary>
    public ICollection<User> Users { get; set; }

    /// <summary>
    /// Создать тестовую сущность
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="name">Название чата</param>
    /// <param name="messages"></param>
    /// <param name="users"></param>
    /// <returns></returns>
    [Obsolete("Только для тестов")]
    public static Chat CreateForTest(
        Guid id = default,
        string? name = default,
        List<Message>? messages = default,
        List<User>? users = default)
        => new()
        {
            Id = id,
            Name = name ?? string.Empty,
            Messages = messages ?? new List<Message>(),
            Users = users ?? new List<User>()
        };
}