using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlpaStock.Core.Migrations
{
    /// <inheritdoc />
    public partial class updatemessagechannelrel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityChannelMessages_ChannelCategories_ChannelId",
                table: "CommunityChannelMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityChannelMessages_CommunityChannels_CommunityChannel~",
                table: "CommunityChannelMessages");

            migrationBuilder.DropIndex(
                name: "IX_CommunityChannelMessages_CommunityChannelId",
                table: "CommunityChannelMessages");

            migrationBuilder.DropColumn(
                name: "CommunityChannelId",
                table: "CommunityChannelMessages");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityChannelMessages_CommunityChannels_ChannelId",
                table: "CommunityChannelMessages",
                column: "ChannelId",
                principalTable: "CommunityChannels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityChannelMessages_CommunityChannels_ChannelId",
                table: "CommunityChannelMessages");

            migrationBuilder.AddColumn<string>(
                name: "CommunityChannelId",
                table: "CommunityChannelMessages",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommunityChannelMessages_CommunityChannelId",
                table: "CommunityChannelMessages",
                column: "CommunityChannelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityChannelMessages_ChannelCategories_ChannelId",
                table: "CommunityChannelMessages",
                column: "ChannelId",
                principalTable: "ChannelCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityChannelMessages_CommunityChannels_CommunityChannel~",
                table: "CommunityChannelMessages",
                column: "CommunityChannelId",
                principalTable: "CommunityChannels",
                principalColumn: "Id");
        }
    }
}
