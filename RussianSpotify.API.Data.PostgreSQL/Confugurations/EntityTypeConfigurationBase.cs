using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotify.API.Core.Entities;

namespace RussianSpotift.API.Data.PostgreSQL.Confugurations;

/// <summary>
/// Базовый класс для настроек сущностей
/// </summary>
public abstract class EntityTypeConfigurationBase<TEntity>
    : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        ConfigureChild(builder);
    }

    /// <summary>
    /// Конфигурация дочерних свойств
    /// </summary>
    /// <param name="builder">Конфигуратор</param>
    protected abstract void ConfigureChild(EntityTypeBuilder<TEntity> builder);
}