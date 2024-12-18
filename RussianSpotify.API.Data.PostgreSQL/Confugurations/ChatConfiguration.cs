using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Shared.Data.PostgreSQL.EntityTypeConfiguration;

namespace RussianSpotift.API.Data.PostgreSQL.Confugurations;

/// <summary>
/// Конфигурация для <see cref="Chat"/>
/// </summary>
public class ChatConfiguration : EntityTypeConfigurationBase<Chat>
{
    /// <inheritdoc>
    ///     <cref>EntityTypeConfigurationBase</cref>
    /// </inheritdoc>
    protected override void ConfigureChild(EntityTypeBuilder<Chat> builder)
    {
        builder.Property(p => p.Name)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasComment("Название чата")
            .IsRequired();

        builder.HasMany(x => x.Messages)
            .WithOne(y => y.Chat)
            .HasForeignKey(y => y.ChatId)
            .HasPrincipalKey(x => x.Id);

        builder.HasMany(x => x.Users)
            .WithMany(y => y.Chats);
    }
}