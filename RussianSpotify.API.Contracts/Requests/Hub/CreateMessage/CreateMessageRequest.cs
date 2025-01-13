namespace RussianSpotify.Contracts.Requests.Hub.CreateMessage;

/// <summary>
///     Запрос на создание сообщения
/// </summary>
public class CreateMessageRequest
{
    /// <summary>
    ///     Сообщение
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    ///     Чат
    /// </summary>
    public Guid ChatId { get; set; }
}