using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlpaStock.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBlogEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "BlogPosts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "BlogPosts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "BlogPosts");
        }
    }
}
