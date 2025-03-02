using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlpaStock.Core.Migrations
{
    /// <inheritdoc />
    public partial class adjustfpreignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionFeatures_Subscriptions_SubscriptionId",
                table: "SubscriptionFeatures");

            migrationBuilder.DropColumn(
                name: "SubcriptionId",
                table: "SubscriptionFeatures");

            migrationBuilder.AlterColumn<string>(
                name: "SubscriptionId",
                table: "SubscriptionFeatures",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionFeatures_Subscriptions_SubscriptionId",
                table: "SubscriptionFeatures",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionFeatures_Subscriptions_SubscriptionId",
                table: "SubscriptionFeatures");

            migrationBuilder.AlterColumn<string>(
                name: "SubscriptionId",
                table: "SubscriptionFeatures",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "SubcriptionId",
                table: "SubscriptionFeatures",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionFeatures_Subscriptions_SubscriptionId",
                table: "SubscriptionFeatures",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id");
        }
    }
}
