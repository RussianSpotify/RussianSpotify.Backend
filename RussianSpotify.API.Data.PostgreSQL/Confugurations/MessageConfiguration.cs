using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Shared.Data.PostgreSQL.EntityTypeConfiguration;
using RussianSpotify.API.Shared.Data.PostgreSQL.Extensions;

namespace RussianSpotift.API.Data.PostgreSQL.Confugurations;

/// <summary>
/// Конфигурация для <see cref="Message"/>
/// </summary>
public class MessageConfiguration : EntityTypeConfigurationBase<Message>
{
    /// <inheritdoc>
    ///     <cref>EntityTypeConfigurationBase</cref>
    /// </inheritdoc>
    protected override void ConfigureChild(EntityTypeBuilder<Message> builder)
    {
        builder.Property(p => p.MessageText)
            .HasComment("Текст сообщения")
            .IsRequired();
        
        builder.ConfigureTimeTrackableEntity();

        builder.Property(p => p.UserId)
            .HasComment("Идентификатор пользователя")
            .IsRequired();

        builder.Property(p => p.ChatId)
            .HasComment("Идентификатор чата")
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(y => y.Messages)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id);

        builder.HasOne(x => x.Chat)
            .WithMany(y => y.Messages)
            .HasForeignKey(x => x.ChatId)
            .HasPrincipalKey(y => y.Id);
    }
}