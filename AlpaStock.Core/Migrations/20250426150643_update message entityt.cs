using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlpaStock.Core.Migrations
{
    /// <inheritdoc />
    public partial class updatemessageentityt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityChannelMessages_ChannelCategories_ChannelCategoryId",
                table: "CommunityChannelMessages");

            migrationBuilder.RenameColumn(
                name: "ChannelCategoryId",
                table: "CommunityChannelMessages",
                newName: "ChannelId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityChannelMessages_ChannelCategoryId",
                table: "CommunityChannelMessages",
                newName: "IX_CommunityChannelMessages_ChannelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityChannelMessages_ChannelCategories_ChannelId",
                table: "CommunityChannelMessages",
                column: "ChannelId",
                principalTable: "ChannelCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityChannelMessages_ChannelCategories_ChannelId",
                table: "CommunityChannelMessages");

            migrationBuilder.RenameColumn(
                name: "ChannelId",
                table: "CommunityChannelMessages",
                newName: "ChannelCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityChannelMessages_ChannelId",
                table: "CommunityChannelMessages",
                newName: "IX_CommunityChannelMessages_ChannelCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityChannelMessages_ChannelCategories_ChannelCategoryId",
                table: "CommunityChannelMessages",
                column: "ChannelCategoryId",
                principalTable: "ChannelCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
