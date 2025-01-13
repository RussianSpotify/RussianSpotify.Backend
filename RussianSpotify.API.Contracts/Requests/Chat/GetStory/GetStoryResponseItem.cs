namespace RussianSpotify.Contracts.Requests.Chat.GetStory;

/// <summary>
///     Модель списка для <see cref="GetStoryResponse" />
/// </summary>
public class GetStoryResponseItem
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Сообщение
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    ///     Отправитель
    /// </summary>
    public string Sender { get; set; }

    /// <summary>
    ///     Идентификатор отправителя
    /// </summary>
    public Guid SenderId { get; set; }

    /// <summary>
    ///     Идентификатор фото отправителя
    /// </summary>
    public Guid? SenderImageId { get; set; }

    /// <summary>
    ///     Дата отправки
    /// </summary>
    public DateTime SentDate { get; set; }
}