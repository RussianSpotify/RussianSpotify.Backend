using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RussianSpotift.API.Data.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");
            
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_name = table.Column<int>(type: "integer", nullable: false, comment: "Название категории"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Дата создания"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Удален"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "email_notifications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    body = table.Column<string>(type: "text", nullable: false, comment: "Тело сообщения"),
                    head = table.Column<string>(type: "text", nullable: false, comment: "Заголовок сообщения"),
                    sent_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата отправки сообщения"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Дата создания"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Удален"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата удаления"),
                    email_to = table.Column<string>(type: "text", nullable: false, comment: "Кому отправлять")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_email_notifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false, comment: "Название роли")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "privileges",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    privilege = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_privileges", x => x.id);
                    table.ForeignKey(
                        name: "fk_privileges_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bucket_song",
                columns: table => new
                {
                    buckets_id = table.Column<Guid>(type: "uuid", nullable: false),
                    songs_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bucket_song", x => new { x.buckets_id, x.songs_id });
                });

            migrationBuilder.CreateTable(
                name: "buckets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Удален"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата удаления"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Дата создания"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_buckets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false, comment: "Адрес файла в S3"),
                    size = table.Column<long>(type: "bigint", nullable: false, comment: "Размер файла"),
                    file_name = table.Column<string>(type: "text", nullable: true, comment: "Название файла"),
                    content_type = table.Column<string>(type: "text", nullable: true, comment: "Тип файла"),
                    song_id = table.Column<Guid>(type: "uuid", nullable: true),
                    playlist_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Дата создания"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Удален"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_files", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "songs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    song_name = table.Column<string>(type: "text", nullable: false, comment: "Название песни"),
                    duration = table.Column<double>(type: "double precision", nullable: false, comment: "Длительность"),
                    plays_number = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "Кол-во прослушиваний"),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    image_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Удален"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата удаления"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Дата создания"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_songs", x => x.id);
                    table.ForeignKey(
                        name: "fk_songs_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_songs_files_image_id",
                        column: x => x.image_id,
                        principalTable: "files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "text", nullable: false, comment: "Логин пользователя"),
                    email = table.Column<string>(type: "text", nullable: false, comment: "Почта пользователя"),
                    password_hash = table.Column<string>(type: "text", nullable: false, comment: "Хеш пароля"),
                    access_token = table.Column<string>(type: "text", nullable: true),
                    refresh_token = table.Column<string>(type: "text", nullable: true),
                    refresh_token_expiry_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    user_photo_id = table.Column<Guid>(type: "uuid", nullable: true),
                    birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    is_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Дата создания"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Удален"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_files_user_photo_id1",
                        column: x => x.user_photo_id,
                        principalTable: "files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "playlists",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    playlist_name = table.Column<string>(type: "text", nullable: false, comment: "Название плейлиста"),
                    image_id = table.Column<Guid>(type: "uuid", nullable: true),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    release_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата релиза"),
                    plays_number = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    is_album = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Дата создания"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Удален"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_playlists", x => x.id);
                    table.ForeignKey(
                        name: "fk_playlists_files_image_id",
                        column: x => x.image_id,
                        principalTable: "files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_playlists_users_author_id",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_user",
                columns: table => new
                {
                    roles_id = table.Column<Guid>(type: "uuid", nullable: false),
                    users_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_user", x => new { x.roles_id, x.users_id });
                    table.ForeignKey(
                        name: "fk_role_user_roles_roles_id",
                        column: x => x.roles_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_role_user_users_users_id",
                        column: x => x.users_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "song_user",
                columns: table => new
                {
                    authors_id = table.Column<Guid>(type: "uuid", nullable: false),
                    songs_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_song_user", x => new { x.authors_id, x.songs_id });
                    table.ForeignKey(
                        name: "fk_song_user_songs_songs_id",
                        column: x => x.songs_id,
                        principalTable: "songs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_song_user_users_authors_id",
                        column: x => x.authors_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscribes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Начало подписки"),
                    date_end = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Конец подписки"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "ИД Пользователь"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Удален"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата удаления"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Дата создания"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subscribes", x => x.id);
                    table.ForeignKey(
                        name: "fk_subscribes_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "playlist_song",
                columns: table => new
                {
                    playlists_id = table.Column<Guid>(type: "uuid", nullable: false),
                    songs_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_playlist_song", x => new { x.playlists_id, x.songs_id });
                    table.ForeignKey(
                        name: "fk_playlist_song_playlists_playlists_id",
                        column: x => x.playlists_id,
                        principalTable: "playlists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_playlist_song_songs_songs_id",
                        column: x => x.songs_id,
                        principalTable: "songs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "playlist_user",
                columns: table => new
                {
                    playlist_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    added_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_playlist_user", x => new { x.playlist_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_playlist_user_playlists_playlist_id",
                        column: x => x.playlist_id,
                        principalTable: "playlists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_playlist_user_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_bucket_song_songs_id",
                table: "bucket_song",
                column: "songs_id");

            migrationBuilder.CreateIndex(
                name: "ix_buckets_user_id",
                table: "buckets",
                column: "user_id",
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

            migrationBuilder.CreateIndex(
                name: "ix_playlist_song_songs_id",
                table: "playlist_song",
                column: "songs_id");

            migrationBuilder.CreateIndex(
                name: "ix_playlist_user_user_id",
                table: "playlist_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_playlists_author_id",
                table: "playlists",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_playlists_image_id",
                table: "playlists",
                column: "image_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_privileges_role_id",
                table: "privileges",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_user_users_id",
                table: "role_user",
                column: "users_id");

            migrationBuilder.CreateIndex(
                name: "ix_song_user_songs_id",
                table: "song_user",
                column: "songs_id");

            migrationBuilder.CreateIndex(
                name: "ix_songs_category_id",
                table: "songs",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_songs_image_id",
                table: "songs",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "ix_subscribes_user_id",
                table: "subscribes",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_user_photo_id",
                table: "users",
                column: "user_photo_id",
                unique: true);

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
                name: "fk_files_playlists_playlist_id",
                table: "files",
                column: "playlist_id",
                principalTable: "playlists",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_files_songs_song_id",
                table: "files",
                column: "song_id",
                principalTable: "songs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_files_users_user_id",
                table: "files",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_files_songs_song_id",
                table: "files");

            migrationBuilder.DropForeignKey(
                name: "fk_files_users_user_id",
                table: "files");

            migrationBuilder.DropForeignKey(
                name: "fk_playlists_users_author_id",
                table: "playlists");

            migrationBuilder.DropForeignKey(
                name: "fk_files_playlists_playlist_id",
                table: "files");

            migrationBuilder.DropTable(
                name: "bucket_song");

            migrationBuilder.DropTable(
                name: "email_notifications");

            migrationBuilder.DropTable(
                name: "playlist_song");

            migrationBuilder.DropTable(
                name: "playlist_user");

            migrationBuilder.DropTable(
                name: "privileges");

            migrationBuilder.DropTable(
                name: "role_user");

            migrationBuilder.DropTable(
                name: "song_user");

            migrationBuilder.DropTable(
                name: "subscribes");

            migrationBuilder.DropTable(
                name: "buckets");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "songs");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "playlists");

            migrationBuilder.DropTable(
                name: "files");
        }
    }
}
