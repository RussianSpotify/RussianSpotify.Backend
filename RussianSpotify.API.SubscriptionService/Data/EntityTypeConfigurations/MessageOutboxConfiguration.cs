using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotify.API.Shared.Data.PostgreSQL.EntityTypeConfiguration;
using RussianSpotify.Grpc.SubscriptionService.Domain.Entities;

namespace RussianSpotify.Grpc.SubscriptionService.Data.EntityTypeConfigurations;

public class MessageOutboxConfiguration : EntityTypeConfigurationBase<MessageOutbox>
{
    protected override void ConfigureChild(EntityTypeBuilder<MessageOutbox> builder)
    {
        builder.Property(p => p.Payload)
            .HasComment("Содержимое сообщения в json формате")
            .IsRequired();
        
        builder.Property(p => p.IsSent)
            .HasComment("Отправлено ли сообщение")
            .IsRequired();
    }
}