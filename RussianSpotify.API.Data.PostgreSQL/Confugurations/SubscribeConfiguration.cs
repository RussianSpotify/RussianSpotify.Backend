using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Shared.Data.PostgreSQL.EntityTypeConfiguration;
using RussianSpotify.API.Shared.Data.PostgreSQL.Extensions;

namespace RussianSpotift.API.Data.PostgreSQL.Confugurations;

/// <summary>
/// Конфигурация для <see cref="Subscribe"/>
/// </summary>
public class SubscribeConfiguration : EntityTypeConfigurationBase<Subscribe>
{
    /// <inheritdoc />
    protected override void ConfigureChild(EntityTypeBuilder<Subscribe> builder)
    {
        builder.Property(p => p.DateStart)
            .HasComment("Начало подписки");

        builder.Property(p => p.DateEnd)
            .HasComment("Конец подписки");
        
        builder.ConfigureSoftDeletableEntity();
        builder.ConfigureTimeTrackableEntity();

        builder.Property(p => p.UserId)
            .HasComment("ИД Пользователь");
        
        builder.HasOne(x => x.User)
            .WithOne(x => x.Subscribe);
    }
}