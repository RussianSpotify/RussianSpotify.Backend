using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RussianSpotift.API.Data.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFileEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_bucket_song_buckets_buckets_id",
                table: "bucket_song");

            migrationBuilder.DropForeignKey(
                name: "fk_bucket_song_songs_songs_id",
                table: "bucket_song");

            migrationBuilder.DropForeignKey(
                name: "fk_buckets_users_user_id",
                table: "buckets");

            migrationBuilder.DropForeignKey(
                name: "fk_chat_user_chats_chats_id",
                table: "chat_user");

            migrationBuilder.DropForeignKey(
                name: "fk_chat_user_users_users_id",
                table: "chat_user");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_chats_chat_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_users_user_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "fk_playlist_song_playlists_playlists_id",
                table: "playlist_song");

            migrationBuilder.DropForeignKey(
                name: "fk_playlist_song_songs_songs_id",
                table: "playlist_song");

            migrationBuilder.DropForeignKey(
                name: "fk_playlist_user_playlists_playlist_id",
                table: "playlist_user");

            migrationBuilder.DropForeignKey(
                name: "fk_playlist_user_users_user_id",
                table: "playlist_user");

            migrationBuilder.DropForeignKey(
                name: "fk_playlists_files_image_id",
                table: "playlists");

            migrationBuilder.DropForeignKey(
                name: "fk_playlists_users_author_id",
                table: "playlists");

            migrationBuilder.DropForeignKey(
                name: "fk_privileges_roles_role_id",
                table: "privileges");

            migrationBuilder.DropForeignKey(
                name: "fk_role_user_roles_roles_id",
                table: "role_user");

            migrationBuilder.DropForeignKey(
                name: "fk_role_user_users_users_id",
                table: "role_user");

            migrationBuilder.DropForeignKey(
                name: "fk_song_user_songs_songs_id",
                table: "song_user");

            migrationBuilder.DropForeignKey(
                name: "fk_song_user_users_authors_id",
                table: "song_user");

            migrationBuilder.DropForeignKey(
                name: "fk_songs_categories_category_id",
                table: "songs");

            migrationBuilder.DropForeignKey(
                name: "fk_songs_files_image_id",
                table: "songs");

            migrationBuilder.DropForeignKey(
                name: "fk_subscribes_users_user_id",
                table: "subscribes");

            migrationBuilder.DropForeignKey(
                name: "fk_users_files_user_photo_id1",
                table: "users");

            migrationBuilder.DropTable(
                name: "files");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_user_photo_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_subscribes",
                table: "subscribes");

            migrationBuilder.DropPrimaryKey(
                name: "pk_songs",
                table: "songs");

            migrationBuilder.DropIndex(
                name: "ix_songs_image_id",
                table: "songs");

            migrationBuilder.DropPrimaryKey(
                name: "pk_roles",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_privileges",
                table: "privileges");

            migrationBuilder.DropPrimaryKey(
                name: "pk_playlists",
                table: "playlists");

            migrationBuilder.DropIndex(
                name: "ix_playlists_image_id",
                table: "playlists");

            migrationBuilder.DropPrimaryKey(
                name: "pk_messages",
                table: "messages");

            migrationBuilder.DropPrimaryKey(
                name: "pk_chats",
                table: "chats");

            migrationBuilder.DropPrimaryKey(
                name: "pk_categories",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_buckets",
                table: "buckets");

            migrationBuilder.DropPrimaryKey(
                name: "pk_song_user",
                table: "song_user");

            migrationBuilder.DropPrimaryKey(
                name: "pk_role_user",
                table: "role_user");

            migrationBuilder.DropPrimaryKey(
                name: "pk_playlist_user",
                table: "playlist_user");

            migrationBuilder.DropPrimaryKey(
                name: "pk_playlist_song",
                table: "playlist_song");

            migrationBuilder.DropPrimaryKey(
                name: "pk_email_notifications",
                table: "email_notifications");

            migrationBuilder.DropPrimaryKey(
                name: "pk_chat_user",
                table: "chat_user");

            migrationBuilder.DropPrimaryKey(
                name: "pk_bucket_song",
                table: "bucket_song");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "subscribes",
                newName: "Subscribes");

            migrationBuilder.RenameTable(
                name: "songs",
                newName: "Songs");

            migrationBuilder.RenameTable(
                name: "roles",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "privileges",
                newName: "Privileges");

            migrationBuilder.RenameTable(
                name: "playlists",
                newName: "Playlists");

            migrationBuilder.RenameTable(
                name: "messages",
                newName: "Messages");

            migrationBuilder.RenameTable(
                name: "chats",
                newName: "Chats");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "buckets",
                newName: "Buckets");

            migrationBuilder.RenameTable(
                name: "song_user",
                newName: "SongUser");

            migrationBuilder.RenameTable(
                name: "role_user",
                newName: "RoleUser");

            migrationBuilder.RenameTable(
                name: "playlist_user",
                newName: "PlaylistUser");

            migrationBuilder.RenameTable(
                name: "playlist_song",
                newName: "PlaylistSong");

            migrationBuilder.RenameTable(
                name: "email_notifications",
                newName: "EmailNotifications");

            migrationBuilder.RenameTable(
                name: "chat_user",
                newName: "ChatUser");

            migrationBuilder.RenameTable(
                name: "bucket_song",
                newName: "BucketSong");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Users",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "birthday",
                table: "Users",
                newName: "Birthday");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_photo_id",
                table: "Users",
                newName: "UserPhotoId");

            migrationBuilder.RenameColumn(
                name: "user_name",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "refresh_token_expiry_time",
                table: "Users",
                newName: "RefreshTokenExpiryTime");

            migrationBuilder.RenameColumn(
                name: "refresh_token",
                table: "Users",
                newName: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "Users",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "is_confirmed",
                table: "Users",
                newName: "IsConfirmed");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "Users",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "access_token",
                table: "Users",
                newName: "AccessToken");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Subscribes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Subscribes",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Subscribes",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "Subscribes",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "Subscribes",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "date_start",
                table: "Subscribes",
                newName: "DateStart");

            migrationBuilder.RenameColumn(
                name: "date_end",
                table: "Subscribes",
                newName: "DateEnd");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Subscribes",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_subscribes_user_id",
                table: "Subscribes",
                newName: "IX_Subscribes_UserId");

            migrationBuilder.RenameColumn(
                name: "duration",
                table: "Songs",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Songs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Songs",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "song_name",
                table: "Songs",
                newName: "SongName");

            migrationBuilder.RenameColumn(
                name: "plays_number",
                table: "Songs",
                newName: "PlaysNumber");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "Songs",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "Songs",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Songs",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "Songs",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "image_id",
                table: "Songs",
                newName: "SongFileId");

            migrationBuilder.RenameIndex(
                name: "ix_songs_category_id",
                table: "Songs",
                newName: "IX_Songs_CategoryId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Roles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "privilege",
                table: "Privileges",
                newName: "Privilege");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Privileges",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "Privileges",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "ix_privileges_role_id",
                table: "Privileges",
                newName: "IX_Privileges_RoleId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Playlists",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Playlists",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "release_date",
                table: "Playlists",
                newName: "ReleaseDate");

            migrationBuilder.RenameColumn(
                name: "plays_number",
                table: "Playlists",
                newName: "PlaysNumber");

            migrationBuilder.RenameColumn(
                name: "playlist_name",
                table: "Playlists",
                newName: "PlaylistName");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "Playlists",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "is_album",
                table: "Playlists",
                newName: "IsAlbum");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "Playlists",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Playlists",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "author_id",
                table: "Playlists",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "image_id",
                table: "Playlists",
                newName: "ImageFileId");

            migrationBuilder.RenameIndex(
                name: "ix_playlists_author_id",
                table: "Playlists",
                newName: "IX_Playlists_AuthorId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Messages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Messages",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Messages",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "message_text",
                table: "Messages",
                newName: "MessageText");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Messages",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "chat_id",
                table: "Messages",
                newName: "ChatId");

            migrationBuilder.RenameIndex(
                name: "ix_messages_user_id",
                table: "Messages",
                newName: "IX_Messages_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_messages_chat_id",
                table: "Messages",
                newName: "IX_Messages_ChatId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Chats",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Chats",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Categories",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "Categories",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "Categories",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Categories",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "category_name",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Buckets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Buckets",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Buckets",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "Buckets",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "Buckets",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Buckets",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_buckets_user_id",
                table: "Buckets",
                newName: "IX_Buckets_UserId");

            migrationBuilder.RenameColumn(
                name: "songs_id",
                table: "SongUser",
                newName: "SongsId");

            migrationBuilder.RenameColumn(
                name: "authors_id",
                table: "SongUser",
                newName: "AuthorsId");

            migrationBuilder.RenameIndex(
                name: "ix_song_user_songs_id",
                table: "SongUser",
                newName: "IX_SongUser_SongsId");

            migrationBuilder.RenameColumn(
                name: "users_id",
                table: "RoleUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "roles_id",
                table: "RoleUser",
                newName: "RolesId");

            migrationBuilder.RenameIndex(
                name: "ix_role_user_users_id",
                table: "RoleUser",
                newName: "IX_RoleUser_UsersId");

            migrationBuilder.RenameColumn(
                name: "added_date",
                table: "PlaylistUser",
                newName: "AddedDate");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "PlaylistUser",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "playlist_id",
                table: "PlaylistUser",
                newName: "PlaylistId");

            migrationBuilder.RenameIndex(
                name: "ix_playlist_user_user_id",
                table: "PlaylistUser",
                newName: "IX_PlaylistUser_UserId");

            migrationBuilder.RenameColumn(
                name: "songs_id",
                table: "PlaylistSong",
                newName: "SongsId");

            migrationBuilder.RenameColumn(
                name: "playlists_id",
                table: "PlaylistSong",
                newName: "PlaylistsId");

            migrationBuilder.RenameIndex(
                name: "ix_playlist_song_songs_id",
                table: "PlaylistSong",
                newName: "IX_PlaylistSong_SongsId");

            migrationBuilder.RenameColumn(
                name: "head",
                table: "EmailNotifications",
                newName: "Head");

            migrationBuilder.RenameColumn(
                name: "body",
                table: "EmailNotifications",
                newName: "Body");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "EmailNotifications",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "EmailNotifications",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "sent_date",
                table: "EmailNotifications",
                newName: "SentDate");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "EmailNotifications",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "email_to",
                table: "EmailNotifications",
                newName: "EmailTo");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "EmailNotifications",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "EmailNotifications",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "users_id",
                table: "ChatUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "chats_id",
                table: "ChatUser",
                newName: "ChatsId");

            migrationBuilder.RenameIndex(
                name: "ix_chat_user_users_id",
                table: "ChatUser",
                newName: "IX_ChatUser_UsersId");

            migrationBuilder.RenameColumn(
                name: "songs_id",
                table: "BucketSong",
                newName: "SongsId");

            migrationBuilder.RenameColumn(
                name: "buckets_id",
                table: "BucketSong",
                newName: "BucketsId");

            migrationBuilder.RenameIndex(
                name: "ix_bucket_song_songs_id",
                table: "BucketSong",
                newName: "IX_BucketSong_SongsId");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageFileId",
                table: "Songs",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscribes",
                table: "Subscribes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Songs",
                table: "Songs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Privileges",
                table: "Privileges",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Buckets",
                table: "Buckets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongUser",
                table: "SongUser",
                columns: new[] { "AuthorsId", "SongsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleUser",
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistUser",
                table: "PlaylistUser",
                columns: new[] { "PlaylistId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistSong",
                table: "PlaylistSong",
                columns: new[] { "PlaylistsId", "SongsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailNotifications",
                table: "EmailNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatUser",
                table: "ChatUser",
                columns: new[] { "ChatsId", "UsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BucketSong",
                table: "BucketSong",
                columns: new[] { "BucketsId", "SongsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Buckets_Users_UserId",
                table: "Buckets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BucketSong_Buckets_BucketsId",
                table: "BucketSong",
                column: "BucketsId",
                principalTable: "Buckets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BucketSong_Songs_SongsId",
                table: "BucketSong",
                column: "SongsId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_Chats_ChatsId",
                table: "ChatUser",
                column: "ChatsId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_Users_UsersId",
                table: "ChatUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Users_AuthorId",
                table: "Playlists",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSong_Playlists_PlaylistsId",
                table: "PlaylistSong",
                column: "PlaylistsId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSong_Songs_SongsId",
                table: "PlaylistSong",
                column: "SongsId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistUser_Playlists_PlaylistId",
                table: "PlaylistUser",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistUser_Users_UserId",
                table: "PlaylistUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Privileges_Roles_RoleId",
                table: "Privileges",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Roles_RolesId",
                table: "RoleUser",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Users_UsersId",
                table: "RoleUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Categories_CategoryId",
                table: "Songs",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongUser_Songs_SongsId",
                table: "SongUser",
                column: "SongsId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongUser_Users_AuthorsId",
                table: "SongUser",
                column: "AuthorsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribes_Users_UserId",
                table: "Subscribes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buckets_Users_UserId",
                table: "Buckets");

            migrationBuilder.DropForeignKey(
                name: "FK_BucketSong_Buckets_BucketsId",
                table: "BucketSong");

            migrationBuilder.DropForeignKey(
                name: "FK_BucketSong_Songs_SongsId",
                table: "BucketSong");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_Chats_ChatsId",
                table: "ChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_Users_UsersId",
                table: "ChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Users_AuthorId",
                table: "Playlists");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSong_Playlists_PlaylistsId",
                table: "PlaylistSong");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSong_Songs_SongsId",
                table: "PlaylistSong");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistUser_Playlists_PlaylistId",
                table: "PlaylistUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistUser_Users_UserId",
                table: "PlaylistUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Privileges_Roles_RoleId",
                table: "Privileges");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Roles_RolesId",
                table: "RoleUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Users_UsersId",
                table: "RoleUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Categories_CategoryId",
                table: "Songs");

            migrationBuilder.DropForeignKey(
                name: "FK_SongUser_Songs_SongsId",
                table: "SongUser");

            migrationBuilder.DropForeignKey(
                name: "FK_SongUser_Users_AuthorsId",
                table: "SongUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscribes_Users_UserId",
                table: "Subscribes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscribes",
                table: "Subscribes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Songs",
                table: "Songs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Privileges",
                table: "Privileges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Buckets",
                table: "Buckets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SongUser",
                table: "SongUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleUser",
                table: "RoleUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistUser",
                table: "PlaylistUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistSong",
                table: "PlaylistSong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailNotifications",
                table: "EmailNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatUser",
                table: "ChatUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BucketSong",
                table: "BucketSong");

            migrationBuilder.DropColumn(
                name: "ImageFileId",
                table: "Songs");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Subscribes",
                newName: "subscribes");

            migrationBuilder.RenameTable(
                name: "Songs",
                newName: "songs");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "roles");

            migrationBuilder.RenameTable(
                name: "Privileges",
                newName: "privileges");

            migrationBuilder.RenameTable(
                name: "Playlists",
                newName: "playlists");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "messages");

            migrationBuilder.RenameTable(
                name: "Chats",
                newName: "chats");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.RenameTable(
                name: "Buckets",
                newName: "buckets");

            migrationBuilder.RenameTable(
                name: "SongUser",
                newName: "song_user");

            migrationBuilder.RenameTable(
                name: "RoleUser",
                newName: "role_user");

            migrationBuilder.RenameTable(
                name: "PlaylistUser",
                newName: "playlist_user");

            migrationBuilder.RenameTable(
                name: "PlaylistSong",
                newName: "playlist_song");

            migrationBuilder.RenameTable(
                name: "EmailNotifications",
                newName: "email_notifications");

            migrationBuilder.RenameTable(
                name: "ChatUser",
                newName: "chat_user");

            migrationBuilder.RenameTable(
                name: "BucketSong",
                newName: "bucket_song");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "users",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "users",
                newName: "birthday");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserPhotoId",
                table: "users",
                newName: "user_photo_id");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "users",
                newName: "user_name");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "users",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiryTime",
                table: "users",
                newName: "refresh_token_expiry_time");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "users",
                newName: "refresh_token");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "users",
                newName: "is_deleted");

            migrationBuilder.RenameColumn(
                name: "IsConfirmed",
                table: "users",
                newName: "is_confirmed");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "users",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AccessToken",
                table: "users",
                newName: "access_token");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "subscribes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "subscribes",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "subscribes",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "subscribes",
                newName: "is_deleted");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "subscribes",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                table: "subscribes",
                newName: "date_start");

            migrationBuilder.RenameColumn(
                name: "DateEnd",
                table: "subscribes",
                newName: "date_end");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "subscribes",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Subscribes_UserId",
                table: "subscribes",
                newName: "ix_subscribes_user_id");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "songs",
                newName: "duration");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "songs",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "songs",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "SongName",
                table: "songs",
                newName: "song_name");

            migrationBuilder.RenameColumn(
                name: "PlaysNumber",
                table: "songs",
                newName: "plays_number");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "songs",
                newName: "is_deleted");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "songs",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "songs",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "songs",
                newName: "category_id");

            migrationBuilder.RenameColumn(
                name: "SongFileId",
                table: "songs",
                newName: "image_id");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_CategoryId",
                table: "songs",
                newName: "ix_songs_category_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "roles",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "roles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Privilege",
                table: "privileges",
                newName: "privilege");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "privileges",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "privileges",
                newName: "role_id");

            migrationBuilder.RenameIndex(
                name: "IX_Privileges_RoleId",
                table: "privileges",
                newName: "ix_privileges_role_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "playlists",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "playlists",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "playlists",
                newName: "release_date");

            migrationBuilder.RenameColumn(
                name: "PlaysNumber",
                table: "playlists",
                newName: "plays_number");

            migrationBuilder.RenameColumn(
                name: "PlaylistName",
                table: "playlists",
                newName: "playlist_name");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "playlists",
                newName: "is_deleted");

            migrationBuilder.RenameColumn(
                name: "IsAlbum",
                table: "playlists",
                newName: "is_album");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "playlists",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "playlists",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "playlists",
                newName: "author_id");

            migrationBuilder.RenameColumn(
                name: "ImageFileId",
                table: "playlists",
                newName: "image_id");

            migrationBuilder.RenameIndex(
                name: "IX_Playlists_AuthorId",
                table: "playlists",
                newName: "ix_playlists_author_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "messages",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "messages",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "messages",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "MessageText",
                table: "messages",
                newName: "message_text");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "messages",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "messages",
                newName: "chat_id");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_UserId",
                table: "messages",
                newName: "ix_messages_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ChatId",
                table: "messages",
                newName: "ix_messages_chat_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "chats",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "chats",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "categories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "categories",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "categories",
                newName: "is_deleted");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "categories",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "categories",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "categories",
                newName: "category_name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "buckets",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "buckets",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "buckets",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "buckets",
                newName: "is_deleted");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "buckets",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "buckets",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Buckets_UserId",
                table: "buckets",
                newName: "ix_buckets_user_id");

            migrationBuilder.RenameColumn(
                name: "SongsId",
                table: "song_user",
                newName: "songs_id");

            migrationBuilder.RenameColumn(
                name: "AuthorsId",
                table: "song_user",
                newName: "authors_id");

            migrationBuilder.RenameIndex(
                name: "IX_SongUser_SongsId",
                table: "song_user",
                newName: "ix_song_user_songs_id");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "role_user",
                newName: "users_id");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "role_user",
                newName: "roles_id");

            migrationBuilder.RenameIndex(
                name: "IX_RoleUser_UsersId",
                table: "role_user",
                newName: "ix_role_user_users_id");

            migrationBuilder.RenameColumn(
                name: "AddedDate",
                table: "playlist_user",
                newName: "added_date");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "playlist_user",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "PlaylistId",
                table: "playlist_user",
                newName: "playlist_id");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistUser_UserId",
                table: "playlist_user",
                newName: "ix_playlist_user_user_id");

            migrationBuilder.RenameColumn(
                name: "SongsId",
                table: "playlist_song",
                newName: "songs_id");

            migrationBuilder.RenameColumn(
                name: "PlaylistsId",
                table: "playlist_song",
                newName: "playlists_id");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistSong_SongsId",
                table: "playlist_song",
                newName: "ix_playlist_song_songs_id");

            migrationBuilder.RenameColumn(
                name: "Head",
                table: "email_notifications",
                newName: "head");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "email_notifications",
                newName: "body");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "email_notifications",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "email_notifications",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "SentDate",
                table: "email_notifications",
                newName: "sent_date");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "email_notifications",
                newName: "is_deleted");

            migrationBuilder.RenameColumn(
                name: "EmailTo",
                table: "email_notifications",
                newName: "email_to");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "email_notifications",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "email_notifications",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "chat_user",
                newName: "users_id");

            migrationBuilder.RenameColumn(
                name: "ChatsId",
                table: "chat_user",
                newName: "chats_id");

            migrationBuilder.RenameIndex(
                name: "IX_ChatUser_UsersId",
                table: "chat_user",
                newName: "ix_chat_user_users_id");

            migrationBuilder.RenameColumn(
                name: "SongsId",
                table: "bucket_song",
                newName: "songs_id");

            migrationBuilder.RenameColumn(
                name: "BucketsId",
                table: "bucket_song",
                newName: "buckets_id");

            migrationBuilder.RenameIndex(
                name: "IX_BucketSong_SongsId",
                table: "bucket_song",
                newName: "ix_bucket_song_songs_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_subscribes",
                table: "subscribes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_songs",
                table: "songs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_roles",
                table: "roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_privileges",
                table: "privileges",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_playlists",
                table: "playlists",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_messages",
                table: "messages",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_chats",
                table: "chats",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_categories",
                table: "categories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_buckets",
                table: "buckets",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_song_user",
                table: "song_user",
                columns: new[] { "authors_id", "songs_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_role_user",
                table: "role_user",
                columns: new[] { "roles_id", "users_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_playlist_user",
                table: "playlist_user",
                columns: new[] { "playlist_id", "user_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_playlist_song",
                table: "playlist_song",
                columns: new[] { "playlists_id", "songs_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_email_notifications",
                table: "email_notifications",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_chat_user",
                table: "chat_user",
                columns: new[] { "chats_id", "users_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_bucket_song",
                table: "bucket_song",
                columns: new[] { "buckets_id", "songs_id" });

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    playlist_id = table.Column<Guid>(type: "uuid", nullable: true),
                    song_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    address = table.Column<string>(type: "text", nullable: false, comment: "Адрес файла в S3"),
                    content_type = table.Column<string>(type: "text", nullable: true, comment: "Тип файла"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Дата создания"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата удаления"),
                    file_name = table.Column<string>(type: "text", nullable: true, comment: "Название файла"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Удален"),
                    size = table.Column<long>(type: "bigint", nullable: false, comment: "Размер файла"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_files", x => x.id);
                    table.ForeignKey(
                        name: "fk_files_playlists_playlist_id",
                        column: x => x.playlist_id,
                        principalTable: "playlists",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_files_songs_song_id",
                        column: x => x.song_id,
                        principalTable: "songs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_files_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_user_photo_id",
                table: "users",
                column: "user_photo_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_songs_image_id",
                table: "songs",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "ix_playlists_image_id",
                table: "playlists",
                column: "image_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_files_playlist_id",
                table: "files",
                column: "playlist_id");

            migrationBuilder.CreateIndex(
                name: "ix_files_song_id",
                table: "files",
                column: "song_id");

            migrationBuilder.CreateIndex(
                name: "ix_files_user_id",
                table: "files",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_bucket_song_buckets_buckets_id",
                table: "bucket_song",
                column: "buckets_id",
                principalTable: "buckets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_bucket_song_songs_songs_id",
                table: "bucket_song",
                column: "songs_id",
                principalTable: "songs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_buckets_users_user_id",
                table: "buckets",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_chat_user_chats_chats_id",
                table: "chat_user",
                column: "chats_id",
                principalTable: "chats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_chat_user_users_users_id",
                table: "chat_user",
                column: "users_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_messages_chats_chat_id",
                table: "messages",
                column: "chat_id",
                principalTable: "chats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_messages_users_user_id",
                table: "messages",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_playlist_song_playlists_playlists_id",
                table: "playlist_song",
                column: "playlists_id",
                principalTable: "playlists",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_playlist_song_songs_songs_id",
                table: "playlist_song",
                column: "songs_id",
                principalTable: "songs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_playlist_user_playlists_playlist_id",
                table: "playlist_user",
                column: "playlist_id",
                principalTable: "playlists",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_playlist_user_users_user_id",
                table: "playlist_user",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_playlists_files_image_id",
                table: "playlists",
                column: "image_id",
                principalTable: "files",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_playlists_users_author_id",
                table: "playlists",
                column: "author_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_privileges_roles_role_id",
                table: "privileges",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_role_user_roles_roles_id",
                table: "role_user",
                column: "roles_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_role_user_users_users_id",
                table: "role_user",
                column: "users_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_song_user_songs_songs_id",
                table: "song_user",
                column: "songs_id",
                principalTable: "songs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_song_user_users_authors_id",
                table: "song_user",
                column: "authors_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_songs_categories_category_id",
                table: "songs",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_songs_files_image_id",
                table: "songs",
                column: "image_id",
                principalTable: "files",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_subscribes_users_user_id",
                table: "subscribes",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_users_files_user_photo_id1",
                table: "users",
                column: "user_photo_id",
                principalTable: "files",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
