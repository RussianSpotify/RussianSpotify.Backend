using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Shared.Data.PostgreSQL.EntityTypeConfiguration;
using RussianSpotify.API.Shared.Data.PostgreSQL.Extensions;

namespace RussianSpotift.API.Data.PostgreSQL.Confugurations;

/// <summary>
/// Конфигурация для <see cref="Bucket"/>
/// </summary>
public class BucketConfiguration : EntityTypeConfigurationBase<Bucket>
{
    /// <inheritdoc />
    protected override void ConfigureChild(EntityTypeBuilder<Bucket> builder)
    {
        builder.ConfigureTimeTrackableEntity();
        builder.ConfigureSoftDeletableEntity();
        
        builder.HasOne(x => x.User)
            .WithOne(y => y.Bucket)
            .HasForeignKey<Bucket>(x => x.UserId)
            .HasPrincipalKey<User>(y => y.Id);

        builder.HasMany(x => x.Songs)
            .WithMany(x => x.Buckets);
    }
}