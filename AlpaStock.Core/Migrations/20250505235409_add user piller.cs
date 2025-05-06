using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlpaStock.Core.Migrations
{
    /// <inheritdoc />
    public partial class adduserpiller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSavePillers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PillerName = table.Column<string>(type: "text", nullable: false),
                    Comparison = table.Column<string>(type: "text", nullable: false),
                    Format = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Userid = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSavePillers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSavePillers_AspNetUsers_Userid",
                        column: x => x.Userid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSavePillers_Userid",
                table: "UserSavePillers",
                column: "Userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSavePillers");
        }
    }
}
