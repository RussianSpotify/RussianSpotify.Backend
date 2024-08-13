using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotift.API.Data.PostgreSQL.Extensions;
using RussianSpotify.API.Core.Entities;

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