using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotift.API.Data.PostgreSQL.Extensions;
using RussianSpotify.API.Core.Entities;

namespace RussianSpotift.API.Data.PostgreSQL.Confugurations;

/// <summary>
/// Конфигурация для <see cref="Category"/>
/// </summary>
public class CategoryConfiguration : EntityTypeConfigurationBase<Category>
{
    /// <inheritdoc />
    protected override void ConfigureChild(EntityTypeBuilder<Category> builder)
    {
        builder.ConfigureTimeTrackableEntity();
        builder.ConfigureSoftDeletableEntity();
        
        builder.Property(p => p.CategoryName)
            .HasComment("Название категории")
            .IsRequired();

        builder.HasMany(y => y.Songs)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .HasPrincipalKey(y => y.Id);
    }
}