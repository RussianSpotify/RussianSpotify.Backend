using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotify.API.Shared.Data.PostgreSQL.EntityTypeConfiguration;
using RussianSpotify.API.Shared.Data.PostgreSQL.Extensions;
using RussianSpotify.Grpc.SubscriptionService.Domain.Entities;

namespace RussianSpotify.Grpc.SubscriptionService.Data.EntityTypeConfigurations;

public class SubscriptionConfiguration : EntityTypeConfigurationBase<Subscription>
{
    protected override void ConfigureChild(EntityTypeBuilder<Subscription> builder)
    {
        builder.ConfigureSoftDeletableEntity();
        builder.ConfigureTimeTrackableEntity();

        builder.Property(p => p.DateStart)
            .HasComment("Начало подписки");

        builder.Property(p => p.DateEnd)
            .HasComment("Конец подписки");
        
        builder.Property(p => p.UserId)
            .HasComment("Идентификатор пользователя")
            .IsRequired();
        
        builder.Property(p => p.Status)
            .HasComment("Статус подписки")
            .IsRequired();

        builder.Property(p => p.FailedReason)
            .HasComment("Сообщение ошибки");
    }
}