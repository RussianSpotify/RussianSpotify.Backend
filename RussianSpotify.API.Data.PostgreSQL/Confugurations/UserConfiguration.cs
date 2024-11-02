using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotift.API.Data.PostgreSQL.Extensions;
using RussianSpotify.API.Core.Entities;

namespace RussianSpotift.API.Data.PostgreSQL.Confugurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.UserName)
            .HasComment("Логин пользователя")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasComment("Почта пользователя")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(p => p.PasswordHash)
            .HasComment("Хеш пароля")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();
        
        builder.Property(p => p.Birthday);

        builder.Property(p => p.Phone);

        builder.Property(p => p.IsConfirmed)
            .IsRequired();
        
        builder.ConfigureSoftDeletableEntity();
        builder.ConfigureTimeTrackableEntity();

        builder.HasMany(x => x.Roles)
            .WithMany(y => y.Users);

        builder.HasMany(x => x.Messages)
            .WithOne(y => y.User)
            .HasForeignKey(y => y.UserId)
            .HasPrincipalKey(x => x.Id);

        builder.HasMany(x => x.Chats)
            .WithMany(y => y.Users);
        
        builder
            .HasOne(x => x.Subscribe)
            .WithOne(y => y.User)
            .HasForeignKey<Subscribe>(y => y.UserId)
            .HasPrincipalKey<User>(x => x.Id);

        builder
            .HasMany(x => x.AuthorPlaylists)
            .WithOne(y => y.Author)
            .HasForeignKey(y => y.AuthorId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(i => i.Playlists)
            .WithMany(i => i.Users)
            .UsingEntity<PlaylistUser>()
            .HasKey(i => new { i.PlaylistId, i.UserId });

        builder
            .HasMany(x => x.Songs)
            .WithMany(y => y.Authors);

        builder.HasOne(i => i.UserPhoto)
            .WithOne()
            .HasForeignKey<User>("UserPhotoId")
            .OnDelete(DeleteBehavior.SetNull);
    }
}