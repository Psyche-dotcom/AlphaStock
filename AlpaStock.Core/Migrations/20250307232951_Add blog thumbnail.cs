using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlpaStock.Core.Migrations
{
    /// <inheritdoc />
    public partial class Addblogthumbnail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlogThumbnailUrl",
                table: "BlogPosts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogThumbnailUrl",
                table: "BlogPosts");
        }
    }
}
