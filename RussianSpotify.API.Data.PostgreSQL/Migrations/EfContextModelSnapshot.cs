﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RussianSpotift.API.Data.PostgreSQL;

#nullable disable

namespace RussianSpotift.API.Data.PostgreSQL.Migrations
{
    [DbContext(typeof(EfContext))]
    partial class EfContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BucketSong", b =>
                {
                    b.Property<Guid>("BucketsId")
                        .HasColumnType("uuid")
                        .HasColumnName("buckets_id");

                    b.Property<Guid>("SongsId")
                        .HasColumnType("uuid")
                        .HasColumnName("songs_id");

                    b.HasKey("BucketsId", "SongsId")
                        .HasName("pk_bucket_song");

                    b.HasIndex("SongsId")
                        .HasDatabaseName("ix_bucket_song_songs_id");

                    b.ToTable("bucket_song", (string)null);
                });

            modelBuilder.Entity("PlaylistSong", b =>
                {
                    b.Property<Guid>("PlaylistsId")
                        .HasColumnType("uuid")
                        .HasColumnName("playlists_id");

                    b.Property<Guid>("SongsId")
                        .HasColumnType("uuid")
                        .HasColumnName("songs_id");

                    b.HasKey("PlaylistsId", "SongsId")
                        .HasName("pk_playlist_song");

                    b.HasIndex("SongsId")
                        .HasDatabaseName("ix_playlist_song_songs_id");

                    b.ToTable("playlist_song", (string)null);
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<Guid>("RolesId")
                        .HasColumnType("uuid")
                        .HasColumnName("roles_id");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid")
                        .HasColumnName("users_id");

                    b.HasKey("RolesId", "UsersId")
                        .HasName("pk_role_user");

                    b.HasIndex("UsersId")
                        .HasDatabaseName("ix_role_user_users_id");

                    b.ToTable("role_user", (string)null);
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Bucket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата создания");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at")
                        .HasComment("Дата удаления");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted")
                        .HasComment("Удален");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at")
                        .HasComment("Дата обновления");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_buckets");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_buckets_user_id");

                    b.ToTable("buckets", (string)null);
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("CategoryName")
                        .HasColumnType("integer")
                        .HasColumnName("category_name")
                        .HasComment("Название категории");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата создания");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at")
                        .HasComment("Дата удаления");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted")
                        .HasComment("Удален");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at")
                        .HasComment("Дата обновления");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.EmailNotification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("body")
                        .HasComment("Тело сообщения");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата создания");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at")
                        .HasComment("Дата удаления");

                    b.Property<string>("EmailTo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email_to")
                        .HasComment("Кому отправлять");

                    b.Property<string>("Head")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("head")
                        .HasComment("Заголовок сообщения");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted")
                        .HasComment("Удален");

                    b.Property<DateTime?>("SentDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("sent_date")
                        .HasComment("Дата отправки сообщения");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at")
                        .HasComment("Дата обновления");

                    b.HasKey("Id")
                        .HasName("pk_email_notifications");

                    b.ToTable("email_notifications", (string)null);
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address")
                        .HasComment("Адрес файла в S3");

                    b.Property<string>("ContentType")
                        .HasColumnType("text")
                        .HasColumnName("content_type")
                        .HasComment("Тип файла");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата создания");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at")
                        .HasComment("Дата удаления");

                    b.Property<string>("FileName")
                        .HasColumnType("text")
                        .HasColumnName("file_name")
                        .HasComment("Название файла");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted")
                        .HasComment("Удален");

                    b.Property<Guid?>("PlaylistId")
                        .HasColumnType("uuid")
                        .HasColumnName("playlist_id");

                    b.Property<long>("Size")
                        .HasColumnType("bigint")
                        .HasColumnName("size")
                        .HasComment("Размер файла");

                    b.Property<Guid?>("SongId")
                        .HasColumnType("uuid")
                        .HasColumnName("song_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at")
                        .HasComment("Дата обновления");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_files");

                    b.HasIndex("PlaylistId")
                        .HasDatabaseName("ix_files_playlist_id");

                    b.HasIndex("SongId")
                        .HasDatabaseName("ix_files_song_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_files_user_id");

                    b.ToTable("files", (string)null);
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Playlist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата создания");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at")
                        .HasComment("Дата удаления");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uuid")
                        .HasColumnName("image_id");

                    b.Property<bool>("IsAlbum")
                        .HasColumnType("boolean")
                        .HasColumnName("is_album");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted")
                        .HasComment("Удален");

                    b.Property<string>("PlaylistName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("playlist_name")
                        .HasComment("Название плейлиста");

                    b.Property<long>("PlaysNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L)
                        .HasColumnName("plays_number");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("release_date")
                        .HasComment("Дата релиза");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at")
                        .HasComment("Дата обновления");

                    b.HasKey("Id")
                        .HasName("pk_playlists");

                    b.HasIndex("AuthorId")
                        .HasDatabaseName("ix_playlists_author_id");

                    b.HasIndex("ImageId")
                        .IsUnique()
                        .HasDatabaseName("ix_playlists_image_id");

                    b.ToTable("playlists", (string)null);
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.PlaylistUser", b =>
                {
                    b.Property<Guid>("PlaylistId")
                        .HasColumnType("uuid")
                        .HasColumnName("playlist_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("added_date");

                    b.HasKey("PlaylistId", "UserId")
                        .HasName("pk_playlist_user");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_playlist_user_user_id");

                    b.ToTable("playlist_user", (string)null);
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name")
                        .HasComment("Название роли");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.RolePrivilege", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Privilege")
                        .HasColumnType("integer")
                        .HasColumnName("privilege");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_privileges");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_privileges_role_id");

                    b.ToTable("privileges", (string)null);
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Song", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата создания");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at")
                        .HasComment("Дата удаления");

                    b.Property<double>("Duration")
                        .HasColumnType("double precision")
                        .HasColumnName("duration")
                        .HasComment("Длительность");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uuid")
                        .HasColumnName("image_id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted")
                        .HasComment("Удален");

                    b.Property<long>("PlaysNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L)
                        .HasColumnName("plays_number")
                        .HasComment("Кол-во прослушиваний");

                    b.Property<string>("SongName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("song_name")
                        .HasComment("Название песни");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at")
                        .HasComment("Дата обновления");

                    b.HasKey("Id")
                        .HasName("pk_songs");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_songs_category_id");

                    b.HasIndex("ImageId")
                        .HasDatabaseName("ix_songs_image_id");

                    b.ToTable("songs", (string)null);
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Subscribe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата создания");

                    b.Property<DateTime?>("DateEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_end")
                        .HasComment("Конец подписки");

                    b.Property<DateTime?>("DateStart")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_start")
                        .HasComment("Начало подписки");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at")
                        .HasComment("Дата удаления");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted")
                        .HasComment("Удален");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at")
                        .HasComment("Дата обновления");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id")
                        .HasComment("ИД Пользователь");

                    b.HasKey("Id")
                        .HasName("pk_subscribes");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_subscribes_user_id");

                    b.ToTable("subscribes", (string)null);
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AccessToken")
                        .HasColumnType("text")
                        .HasColumnName("access_token");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birthday");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата создания");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at")
                        .HasComment("Дата удаления");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email")
                        .HasComment("Почта пользователя");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("is_confirmed");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted")
                        .HasComment("Удален");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash")
                        .HasComment("Хеш пароля");

                    b.Property<string>("Phone")
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text")
                        .HasColumnName("refresh_token");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("refresh_token_expiry_time");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at")
                        .HasComment("Дата обновления");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_name")
                        .HasComment("Логин пользователя");

                    b.Property<Guid?>("UserPhotoId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_photo_id");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("UserPhotoId")
                        .IsUnique()
                        .HasDatabaseName("ix_users_user_photo_id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("SongUser", b =>
                {
                    b.Property<Guid>("AuthorsId")
                        .HasColumnType("uuid")
                        .HasColumnName("authors_id");

                    b.Property<Guid>("SongsId")
                        .HasColumnType("uuid")
                        .HasColumnName("songs_id");

                    b.HasKey("AuthorsId", "SongsId")
                        .HasName("pk_song_user");

                    b.HasIndex("SongsId")
                        .HasDatabaseName("ix_song_user_songs_id");

                    b.ToTable("song_user", (string)null);
                });

            modelBuilder.Entity("BucketSong", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.Bucket", null)
                        .WithMany()
                        .HasForeignKey("BucketsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_bucket_song_buckets_buckets_id");

                    b.HasOne("RussianSpotify.API.Core.Entities.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_bucket_song_songs_songs_id");
                });

            modelBuilder.Entity("PlaylistSong", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.Playlist", null)
                        .WithMany()
                        .HasForeignKey("PlaylistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_playlist_song_playlists_playlists_id");

                    b.HasOne("RussianSpotify.API.Core.Entities.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_playlist_song_songs_songs_id");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_user_roles_roles_id");

                    b.HasOne("RussianSpotify.API.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_user_users_users_id");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Bucket", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.User", "User")
                        .WithOne("Bucket")
                        .HasForeignKey("RussianSpotify.API.Core.Entities.Bucket", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_buckets_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.File", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.Playlist", "Playlist")
                        .WithMany()
                        .HasForeignKey("PlaylistId")
                        .HasConstraintName("fk_files_playlists_playlist_id");

                    b.HasOne("RussianSpotify.API.Core.Entities.Song", "Song")
                        .WithMany("Files")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_files_songs_song_id");

                    b.HasOne("RussianSpotify.API.Core.Entities.User", "User")
                        .WithMany("Files")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_files_users_user_id");

                    b.Navigation("Playlist");

                    b.Navigation("Song");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Playlist", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.User", "Author")
                        .WithMany("AuthorPlaylists")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_playlists_users_author_id");

                    b.HasOne("RussianSpotify.API.Core.Entities.File", "Image")
                        .WithOne()
                        .HasForeignKey("RussianSpotify.API.Core.Entities.Playlist", "ImageId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_playlists_files_image_id");

                    b.Navigation("Author");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.PlaylistUser", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.Playlist", "Playlist")
                        .WithMany("PlaylistUsers")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_playlist_user_playlists_playlist_id");

                    b.HasOne("RussianSpotify.API.Core.Entities.User", "User")
                        .WithMany("PlaylistUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_playlist_user_users_user_id");

                    b.Navigation("Playlist");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.RolePrivilege", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.Role", "Role")
                        .WithMany("Privileges")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_privileges_roles_role_id");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Song", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.Category", "Category")
                        .WithMany("Songs")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_songs_categories_category_id");

                    b.HasOne("RussianSpotify.API.Core.Entities.File", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_songs_files_image_id");

                    b.Navigation("Category");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Subscribe", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.User", "User")
                        .WithOne("Subscribe")
                        .HasForeignKey("RussianSpotify.API.Core.Entities.Subscribe", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_subscribes_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.User", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.File", "UserPhoto")
                        .WithOne()
                        .HasForeignKey("RussianSpotify.API.Core.Entities.User", "UserPhotoId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_users_files_user_photo_id1");

                    b.Navigation("UserPhoto");
                });

            modelBuilder.Entity("SongUser", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("AuthorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_song_user_users_authors_id");

                    b.HasOne("RussianSpotify.API.Core.Entities.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_song_user_songs_songs_id");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Category", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Playlist", b =>
                {
                    b.Navigation("PlaylistUsers");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Role", b =>
                {
                    b.Navigation("Privileges");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Song", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.User", b =>
                {
                    b.Navigation("AuthorPlaylists");

                    b.Navigation("Bucket");

                    b.Navigation("Files");

                    b.Navigation("PlaylistUsers");

                    b.Navigation("Subscribe");
                });
#pragma warning restore 612, 618
        }
    }
}
