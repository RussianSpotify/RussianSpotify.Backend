using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RussianSpotify.Grpc.SubscriptionService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageOutboxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Payload = table.Column<string>(type: "text", nullable: false, comment: "Содержимое сообщения в json формате"),
                    IsSent = table.Column<bool>(type: "boolean", nullable: false, comment: "Отправлено ли сообщение"),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageOutboxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FailedReason = table.Column<string>(type: "text", nullable: true, comment: "Сообщение ошибки"),
                    DateStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Начало подписки"),
                    DateEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Конец подписки"),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор пользователя"),
                    Status = table.Column<int>(type: "integer", nullable: false, comment: "Статус подписки"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Удален"),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата удаления"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Дата создания"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageOutboxes");

            migrationBuilder.DropTable(
                name: "Subscriptions");
        }
    }
}
