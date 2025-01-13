#region

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotify.API.Core.Entities;

#endregion

namespace RussianSpotift.API.Data.PostgreSQL.Confugurations;

/// <summary>
///     Конфигурация для <see cref="Role" />
/// </summary>
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(p => p.Name)
            .HasComment("Название роли")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.HasMany(x => x.Users)
            .WithMany(y => y.Roles);

        builder.HasMany(x => x.Privileges)
            .WithOne(y => y.Role)
            .HasForeignKey(y => y.RoleId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}