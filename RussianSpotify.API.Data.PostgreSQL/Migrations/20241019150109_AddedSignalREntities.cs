using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RussianSpotift.API.Data.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddedSignalREntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chats",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false, comment: "Название чата")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chats", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_user",
                columns: table => new
                {
                    chats_id = table.Column<Guid>(type: "uuid", nullable: false),
                    users_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chat_user", x => new { x.chats_id, x.users_id });
                    table.ForeignKey(
                        name: "fk_chat_user_chats_chats_id",
                        column: x => x.chats_id,
                        principalTable: "chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_chat_user_users_users_id",
                        column: x => x.users_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    message_text = table.Column<string>(type: "text", nullable: false, comment: "Текст сообщения"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Дата создания"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор пользователя"),
                    chat_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор чата")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_messages", x => x.id);
                    table.ForeignKey(
                        name: "fk_messages_chats_chat_id",
                        column: x => x.chat_id,
                        principalTable: "chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_messages_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_chat_user_users_id",
                table: "chat_user",
                column: "users_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_chat_id",
                table: "messages",
                column: "chat_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_user_id",
                table: "messages",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chat_user");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "chats");
        }
    }
}
