using RussianSpotify.API.Core.Abstractions;

namespace RussianSpotify.API.Core.Entities;

/// <summary>
/// Сущность уведомления
/// </summary>
public class EmailNotification : BaseEntity, ISoftDeletable, ITimeTrackable
{
    /// <summary>
    /// Содержимое уведомления
    /// </summary>
    public string Body { get; set; } = default!;

    /// <summary>
    /// Голова уведомления
    /// </summary>
    public string Head { get; set; } = default!;

    /// <summary>
    /// Отправлено сообщение
    /// </summary>
    public DateTime? SentDate { get; set; }
    
    /// <inheritdoc />
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTime? UpdatedAt { get; set; }
    
    /// <inheritdoc />
    public bool IsDeleted { get; set; }

    /// <inheritdoc />
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Получатель
    /// </summary>
    public string EmailTo { get; set; } = default!;

    /// <summary>
    /// Создать уведомление
    /// </summary>
    /// <param name="body">Содержимое</param>
    /// <param name="head">Голова сообещния</param>
    /// <param name="emailTo">Кому</param>
    /// <returns>Уведомление</returns>
    public static EmailNotification CreateNotification(
        string body,
        string head,
        string emailTo)
        => new()
        {
            Body = body,
            Head = head,
            EmailTo = emailTo,
            SentDate = null
        };
}