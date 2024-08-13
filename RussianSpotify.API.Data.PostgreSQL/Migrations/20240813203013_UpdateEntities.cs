using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RussianSpotift.API.Data.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Subscribes",
                type: "uuid",
                nullable: false,
                comment: "ИД Пользователь",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateStart",
                table: "Subscribes",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Начало подписки",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEnd",
                table: "Subscribes",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Конец подписки",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Subscribes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                comment: "Дата создания");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Subscribes",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата удаления");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Subscribes",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Удален");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Subscribes",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата обновления");

            migrationBuilder.AlterColumn<string>(
                name: "SongName",
                table: "Songs",
                type: "text",
                nullable: false,
                comment: "Название песни",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "PlaysNumber",
                table: "Songs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "Кол-во прослушиваний",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 0L);

            migrationBuilder.AlterColumn<double>(
                name: "Duration",
                table: "Songs",
                type: "double precision",
                nullable: false,
                comment: "Длительность",
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Songs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                comment: "Дата создания");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Songs",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата удаления");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Songs",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Удален");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Songs",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата обновления");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "Playlists",
                type: "timestamp with time zone",
                nullable: false,
                comment: "Дата релиза",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "PlaylistName",
                table: "Playlists",
                type: "text",
                nullable: false,
                comment: "Название плейлиста",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Playlists",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                comment: "Дата создания");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Playlists",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата удаления");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Playlists",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Удален");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Playlists",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата обновления");

            migrationBuilder.AlterColumn<long>(
                name: "Size",
                table: "Files",
                type: "bigint",
                nullable: false,
                comment: "Размер файла",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Files",
                type: "text",
                nullable: true,
                comment: "Название файла",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "Files",
                type: "text",
                nullable: true,
                comment: "Тип файла",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Files",
                type: "text",
                nullable: false,
                comment: "Адрес файла в S3",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Files",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                comment: "Дата создания");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Files",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата удаления");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Files",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Удален");

            migrationBuilder.AddColumn<Guid>(
                name: "PlaylistId",
                table: "Files",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Files",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата обновления");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentDate",
                table: "EmailNotifications",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата отправки сообщения",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Head",
                table: "EmailNotifications",
                type: "text",
                nullable: false,
                comment: "Заголовок сообщения",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "EmailTo",
                table: "EmailNotifications",
                type: "text",
                nullable: false,
                comment: "Кому отправлять",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "EmailNotifications",
                type: "text",
                nullable: false,
                comment: "Тело сообщения",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EmailNotifications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                comment: "Дата создания");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "EmailNotifications",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата удаления");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EmailNotifications",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Удален");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "EmailNotifications",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата обновления");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryName",
                table: "Categories",
                type: "integer",
                nullable: false,
                comment: "Название категории",
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                comment: "Дата создания");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата удаления");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Удален");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата обновления");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Buckets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                comment: "Дата создания");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Buckets",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата удаления");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Buckets",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Удален");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Buckets",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата обновления");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                comment: "Дата создания");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата удаления");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Удален");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата обновления");

            migrationBuilder.CreateIndex(
                name: "IX_Files_PlaylistId",
                table: "Files",
                column: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Playlists_PlaylistId",
                table: "Files",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Playlists_PlaylistId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_PlaylistId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Subscribes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Subscribes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Subscribes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Subscribes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "PlaylistId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EmailNotifications");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "EmailNotifications");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EmailNotifications");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "EmailNotifications");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Buckets");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Buckets");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Buckets");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Buckets");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Subscribes",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "ИД Пользователь");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateStart",
                table: "Subscribes",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldComment: "Начало подписки");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEnd",
                table: "Subscribes",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldComment: "Конец подписки");

            migrationBuilder.AlterColumn<string>(
                name: "SongName",
                table: "Songs",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Название песни");

            migrationBuilder.AlterColumn<long>(
                name: "PlaysNumber",
                table: "Songs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 0L,
                oldComment: "Кол-во прослушиваний");

            migrationBuilder.AlterColumn<double>(
                name: "Duration",
                table: "Songs",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldComment: "Длительность");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "Playlists",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldComment: "Дата релиза");

            migrationBuilder.AlterColumn<string>(
                name: "PlaylistName",
                table: "Playlists",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Название плейлиста");

            migrationBuilder.AlterColumn<long>(
                name: "Size",
                table: "Files",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "Размер файла");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Files",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "Название файла");

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "Files",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "Тип файла");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Files",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Адрес файла в S3");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentDate",
                table: "EmailNotifications",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldComment: "Дата отправки сообщения");

            migrationBuilder.AlterColumn<string>(
                name: "Head",
                table: "EmailNotifications",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Заголовок сообщения");

            migrationBuilder.AlterColumn<string>(
                name: "EmailTo",
                table: "EmailNotifications",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Кому отправлять");

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "EmailNotifications",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Тело сообщения");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryName",
                table: "Categories",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Название категории");
        }
    }
}
