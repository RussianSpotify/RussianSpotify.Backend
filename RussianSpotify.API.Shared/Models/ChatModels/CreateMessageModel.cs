namespace RussianSpotify.API.Shared.Models.ChatModels;

/// <summary>
///     Запрос на создание сообщения
/// </summary>
public class CreateMessageModel
{
    /// <summary>
    ///     Сообщение
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    ///     Идентификатор чата
    /// </summary>
    public Guid ChatId { get; set; }

    /// <summary>
    ///     Идентификатор отправителя
    /// </summary>
    public Guid UserId { get; set; }
}