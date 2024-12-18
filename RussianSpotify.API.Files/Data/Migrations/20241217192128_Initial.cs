using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RussianSpotify.API.Files.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilesMetadata",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false, comment: "Адрес файла в S3"),
                    Size = table.Column<long>(type: "bigint", nullable: false, comment: "Размер файла"),
                    FileName = table.Column<string>(type: "text", nullable: true, comment: "Название файла"),
                    ContentType = table.Column<string>(type: "text", nullable: true, comment: "Тип файла"),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Дата создания"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Удален"),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesMetadata", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilesMetadata");
        }
    }
}
