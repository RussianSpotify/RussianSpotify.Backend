using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotift.API.Data.PostgreSQL.Extensions;
using RussianSpotify.API.Core.Entities;

namespace RussianSpotift.API.Data.PostgreSQL.Confugurations;

/// <summary>
/// Конфигурация для <see cref="EmailNotification"/>
/// </summary>
public class EmailNotificationConfiguration : EntityTypeConfigurationBase<EmailNotification>
{
    /// <inheritdoc />
    protected override void ConfigureChild(EntityTypeBuilder<EmailNotification> builder)
    {
        builder.ConfigureSoftDeletableEntity();
        builder.ConfigureTimeTrackableEntity();
        
        builder.Property(p => p.EmailTo)
            .HasComment("Кому отправлять")
            .IsRequired();

        builder.Property(p => p.Body)
            .HasComment("Тело сообщения")
            .IsRequired();

        builder.Property(p => p.Head)
            .HasComment("Заголовок сообщения")
            .IsRequired();

        builder.Property(p => p.SentDate)
            .HasComment("Дата отправки сообщения");
    }
}