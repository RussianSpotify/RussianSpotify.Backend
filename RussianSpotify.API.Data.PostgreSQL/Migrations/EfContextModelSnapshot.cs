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
                        .HasColumnType("uuid");

                    b.Property<Guid>("SongsId")
                        .HasColumnType("uuid");

                    b.HasKey("BucketsId", "SongsId");

                    b.HasIndex("SongsId");

                    b.ToTable("BucketSong");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PlaylistSong", b =>
                {
                    b.Property<Guid>("PlaylistsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SongsId")
                        .HasColumnType("uuid");

                    b.HasKey("PlaylistsId", "SongsId");

                    b.HasIndex("SongsId");

                    b.ToTable("PlaylistSong");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Bucket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата создания");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата удаления");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasComment("Удален");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата обновления");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Buckets");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CategoryName")
                        .HasColumnType("integer")
                        .HasComment("Название категории");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата создания");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата удаления");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasComment("Удален");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата обновления");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.EmailNotification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Тело сообщения");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата создания");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата удаления");

                    b.Property<string>("EmailTo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Кому отправлять");

                    b.Property<string>("Head")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Заголовок сообщения");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasComment("Удален");

                    b.Property<DateTime?>("SentDate")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата отправки сообщения");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата обновления");

                    b.HasKey("Id");

                    b.ToTable("EmailNotifications");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Адрес файла в S3");

                    b.Property<string>("ContentType")
                        .HasColumnType("text")
                        .HasComment("Тип файла");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата создания");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата удаления");

                    b.Property<string>("FileName")
                        .HasColumnType("text")
                        .HasComment("Название файла");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasComment("Удален");

                    b.Property<Guid?>("PlaylistId")
                        .HasColumnType("uuid");

                    b.Property<long>("Size")
                        .HasColumnType("bigint")
                        .HasComment("Размер файла");

                    b.Property<Guid?>("SongId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата обновления");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PlaylistId");

                    b.HasIndex("SongId");

                    b.HasIndex("UserId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Playlist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата создания");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата удаления");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsAlbum")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasComment("Удален");

                    b.Property<string>("PlaylistName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Название плейлиста");

                    b.Property<long>("PlaysNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L);

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата релиза");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата обновления");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ImageId")
                        .IsUnique();

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.PlaylistUser", b =>
                {
                    b.Property<Guid>("PlaylistId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("PlaylistId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("PlaylistUser");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.RolePrivilege", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Privilege")
                        .HasColumnType("integer");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Privileges");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Song", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата создания");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата удаления");

                    b.Property<double>("Duration")
                        .HasColumnType("double precision")
                        .HasComment("Длительность");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasComment("Удален");

                    b.Property<long>("PlaysNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L)
                        .HasComment("Кол-во прослушиваний");

                    b.Property<string>("SongName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Название песни");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата обновления");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ImageId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Subscribe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата создания");

                    b.Property<DateTime?>("DateEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Конец подписки");

                    b.Property<DateTime?>("DateStart")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Начало подписки");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата удаления");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasComment("Удален");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата обновления");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasComment("ИД Пользователь");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Subscribes");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("AccessToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата создания");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата удаления");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasComment("Удален");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата обновления");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<Guid?>("UserPhotoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("UserPhotoId")
                        .IsUnique();

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("SongUser", b =>
                {
                    b.Property<Guid>("AuthorsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SongsId")
                        .HasColumnType("uuid");

                    b.HasKey("AuthorsId", "SongsId");

                    b.HasIndex("SongsId");

                    b.ToTable("SongUser");
                });

            modelBuilder.Entity("BucketSong", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.Bucket", null)
                        .WithMany()
                        .HasForeignKey("BucketsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RussianSpotify.API.Core.Entities.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RussianSpotify.API.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PlaylistSong", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.Playlist", null)
                        .WithMany()
                        .HasForeignKey("PlaylistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RussianSpotify.API.Core.Entities.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Bucket", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.User", "User")
                        .WithOne("Bucket")
                        .HasForeignKey("RussianSpotify.API.Core.Entities.Bucket", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.File", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.Playlist", "Playlist")
                        .WithMany()
                        .HasForeignKey("PlaylistId");

                    b.HasOne("RussianSpotify.API.Core.Entities.Song", "Song")
                        .WithMany("Files")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RussianSpotify.API.Core.Entities.User", "User")
                        .WithMany("Files")
                        .HasForeignKey("UserId");

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
                        .IsRequired();

                    b.HasOne("RussianSpotify.API.Core.Entities.File", "Image")
                        .WithOne()
                        .HasForeignKey("RussianSpotify.API.Core.Entities.Playlist", "ImageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Author");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.PlaylistUser", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.Playlist", "Playlist")
                        .WithMany("PlaylistUsers")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RussianSpotify.API.Core.Entities.User", "User")
                        .WithMany("PlaylistUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.RolePrivilege", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.Role", "Role")
                        .WithMany("Privileges")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Song", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.Category", "Category")
                        .WithMany("Songs")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RussianSpotify.API.Core.Entities.File", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Category");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.Subscribe", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.User", "User")
                        .WithOne("Subscribe")
                        .HasForeignKey("RussianSpotify.API.Core.Entities.Subscribe", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RussianSpotify.API.Core.Entities.User", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.File", "UserPhoto")
                        .WithOne()
                        .HasForeignKey("RussianSpotify.API.Core.Entities.User", "UserPhotoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("UserPhoto");
                });

            modelBuilder.Entity("SongUser", b =>
                {
                    b.HasOne("RussianSpotify.API.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("AuthorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RussianSpotify.API.Core.Entities.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
