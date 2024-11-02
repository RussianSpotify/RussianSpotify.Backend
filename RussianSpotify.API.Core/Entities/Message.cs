using RussianSpotify.API.Core.Abstractions;

namespace RussianSpotify.API.Core.Entities;

/// <summary>
/// Сообщение
/// </summary>
public class Message : BaseEntity, ITimeTrackable
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="messageText">Текст сообщения</param>
    /// <param name="user">Пользователь, кто отправил</param>
    /// <param name="chat">Чат</param>
    public Message(string messageText, User user, Chat chat)
    {
        MessageText = messageText;
        User = user;
        Chat = chat;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    private Message()
    {
    }

    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string MessageText { get; set; }

    /// <inheritdoc cref="ITimeTrackable"/>
    public DateTime CreatedAt { get; set; }
    
    /// <inheritdoc cref="ITimeTrackable"/>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Идентификатор пользователя 
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public Guid ChatId { get; set; }
    
    /// <summary>
    /// Пользователь, кто отправил
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// Чат
    /// </summary>
    public Chat Chat { get; set; }

    /// <summary>
    /// Создать тестовую сущность
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="messageText">Текст сообщения</param>
    /// <param name="user">Пользователь, кто отправил</param>
    /// <param name="chat">Чат</param>
    /// <returns>Тестовая сущность</returns>
    [Obsolete("Только для тестов")]
    public static Message CreateForTest(
        Guid id = default,
        string? messageText = default,
        User? user = default,
        Chat? chat = default)
        => new()
        {
            Id = id,
            MessageText = messageText ?? string.Empty,
            UserId = user?.Id ?? default,
            ChatId = chat?.Id ?? default,
        };
}