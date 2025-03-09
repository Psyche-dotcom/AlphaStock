using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlpaStock.Core.Migrations
{
    /// <inheritdoc />
    public partial class addcommunityentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChannelCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelCategories_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommunityChannels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<string>(type: "text", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunityChannels_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityChannels_ChannelCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ChannelCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommunityChannelMessages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ChannelCategoryId = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    IsLiked = table.Column<bool>(type: "boolean", nullable: false),
                    IsUnLiked = table.Column<bool>(type: "boolean", nullable: false),
                    MessageType = table.Column<string>(type: "text", nullable: false),
                    SentById = table.Column<string>(type: "text", nullable: false),
                    CommunityChannelId = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityChannelMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunityChannelMessages_AspNetUsers_SentById",
                        column: x => x.SentById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityChannelMessages_ChannelCategories_ChannelCategoryId",
                        column: x => x.ChannelCategoryId,
                        principalTable: "ChannelCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityChannelMessages_CommunityChannels_CommunityChannel~",
                        column: x => x.CommunityChannelId,
                        principalTable: "CommunityChannels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserCommunityChannels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CommunityChannelId = table.Column<string>(type: "text", nullable: false),
                    IsSuspended = table.Column<bool>(type: "boolean", nullable: false),
                    SuspendedBy = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCommunityChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCommunityChannels_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCommunityChannels_CommunityChannels_CommunityChannelId",
                        column: x => x.CommunityChannelId,
                        principalTable: "CommunityChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChannelMesageReplies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ChannelMessageId = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    MessageType = table.Column<string>(type: "text", nullable: false),
                    SentById = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelMesageReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelMesageReplies_AspNetUsers_SentById",
                        column: x => x.SentById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelMesageReplies_CommunityChannelMessages_ChannelMessag~",
                        column: x => x.ChannelMessageId,
                        principalTable: "CommunityChannelMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChannelMessageLikies",
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
                    table.PrimaryKey("PK_ChannelMessageLikies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelMessageLikies_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelMessageLikies_CommunityChannelMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "CommunityChannelMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChannelMessageUnLikies",
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
                    table.PrimaryKey("PK_ChannelMessageUnLikies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelMessageUnLikies_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelMessageUnLikies_CommunityChannelMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "CommunityChannelMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelCategories_CreatedByUserId",
                table: "ChannelCategories",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelMesageReplies_ChannelMessageId",
                table: "ChannelMesageReplies",
                column: "ChannelMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelMesageReplies_SentById",
                table: "ChannelMesageReplies",
                column: "SentById");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelMessageLikies_MessageId",
                table: "ChannelMessageLikies",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelMessageLikies_UserId",
                table: "ChannelMessageLikies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelMessageUnLikies_MessageId",
                table: "ChannelMessageUnLikies",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelMessageUnLikies_UserId",
                table: "ChannelMessageUnLikies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityChannelMessages_ChannelCategoryId",
                table: "CommunityChannelMessages",
                column: "ChannelCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityChannelMessages_CommunityChannelId",
                table: "CommunityChannelMessages",
                column: "CommunityChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityChannelMessages_SentById",
                table: "CommunityChannelMessages",
                column: "SentById");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityChannels_CategoryId",
                table: "CommunityChannels",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityChannels_CreatedByUserId",
                table: "CommunityChannels",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCommunityChannels_CommunityChannelId",
                table: "UserCommunityChannels",
                column: "CommunityChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCommunityChannels_UserId",
                table: "UserCommunityChannels",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelMesageReplies");

            migrationBuilder.DropTable(
                name: "ChannelMessageLikies");

            migrationBuilder.DropTable(
                name: "ChannelMessageUnLikies");

            migrationBuilder.DropTable(
                name: "UserCommunityChannels");

            migrationBuilder.DropTable(
                name: "CommunityChannelMessages");

            migrationBuilder.DropTable(
                name: "CommunityChannels");

            migrationBuilder.DropTable(
                name: "ChannelCategories");
        }
    }
}
