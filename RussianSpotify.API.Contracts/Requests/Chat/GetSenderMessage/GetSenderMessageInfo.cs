namespace RussianSpotify.Contracts.Requests.Chat.GetSenderMessage;

/// <summary>
/// Ответ при отправке сообщения
/// </summary>
public class GetSenderMessageInfo
{
    /// <summary>
    /// Идентификатор отправителя
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя отправителя
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Идентификатор аватарки
    /// </summary>
    public Guid? ImageId { get; set; }
}