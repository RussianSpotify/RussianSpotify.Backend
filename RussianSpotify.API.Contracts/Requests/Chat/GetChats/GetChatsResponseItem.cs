namespace RussianSpotify.Contracts.Requests.Chat.GetChats;

/// <summary>
///     Элемент списка для <see cref="GetChatsResponse" />
/// </summary>
public class GetChatsResponseItem
{
    /// <summary>
    ///     Идентификатор чата
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Название чата
    /// </summary>
    public string? ChatName { get; set; }

    /// <summary>
    ///     Идентификатор фото чата
    /// </summary>
    public Guid? ImageId { get; set; }

    /// <summary>
    ///     Последнее сообщение
    /// </summary>
    public string? LastMessage { get; set; }
}