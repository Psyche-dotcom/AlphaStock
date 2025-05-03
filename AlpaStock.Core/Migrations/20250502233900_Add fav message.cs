using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlpaStock.Core.Migrations
{
    /// <inheritdoc />
    public partial class Addfavmessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavoriteChannelMessages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    MessageId = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteChannelMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteChannelMessages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteChannelMessages_CommunityChannelMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "CommunityChannelMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteChannelMessages_MessageId",
                table: "FavoriteChannelMessages",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteChannelMessages_UserId",
                table: "FavoriteChannelMessages",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteChannelMessages");
        }
    }
}
