using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotify.API.PaymentService.Domain.Entities;
using RussianSpotify.API.Shared.Data.PostgreSQL.EntityTypeConfiguration;
using RussianSpotify.API.Shared.Data.PostgreSQL.Extensions;

namespace RussianSpotify.API.PaymentService.Data.Configurations;

public class PaymentConfiguration : EntityTypeConfigurationBase<Payment>
{
    protected override void ConfigureChild(EntityTypeBuilder<Payment> builder)
    {
        builder.ConfigureSoftDeletableEntity();
        builder.ConfigureTimeTrackableEntity();

        builder.Property(p => p.Amount)
            .HasComment("Сколько денег внесено")
            .IsRequired();

        builder.Property(p => p.UserId)
            .HasComment("Идентификатор пользователя")
            .IsRequired();

        builder.Property(p => p.SubscriptionId)
            .HasComment("Идентификатор подписки")
            .IsRequired();
    }
}