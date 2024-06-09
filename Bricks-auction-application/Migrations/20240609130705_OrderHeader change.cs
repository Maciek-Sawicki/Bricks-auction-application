using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bricks_auction_application.Migrations
{
    /// <inheritdoc />
    public partial class OrderHeaderchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_Users_User",
                table: "OrderHeaders");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "OrderHeaders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHeaders_User",
                table: "OrderHeaders",
                newName: "IX_OrderHeaders_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_Users_UserId",
                table: "OrderHeaders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_Users_UserId",
                table: "OrderHeaders");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OrderHeaders",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHeaders_UserId",
                table: "OrderHeaders",
                newName: "IX_OrderHeaders_User");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_Users_User",
                table: "OrderHeaders",
                column: "User",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
