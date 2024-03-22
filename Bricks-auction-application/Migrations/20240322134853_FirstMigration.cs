using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bricks_auction_application.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SetId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    OfferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LEGOSetId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OfferEndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.OfferId);
                    table.ForeignKey(
                        name: "FK_Offers_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offers_Sets_LEGOSetId",
                        column: x => x.LEGOSetId,
                        principalTable: "Sets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersHistories",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersHistories", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrdersHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SellerReviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerUserId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerReviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_SellerReviews_Users_SellerUserId",
                        column: x => x.SellerUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    SetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Sets_SetId",
                        column: x => x.SetId,
                        principalTable: "Sets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderedCarts",
                columns: table => new
                {
                    OrderedCartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrdersHistoryOrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedCarts", x => x.OrderedCartId);
                    table.ForeignKey(
                        name: "FK_OrderedCarts_OrdersHistories_OrdersHistoryOrderId",
                        column: x => x.OrdersHistoryOrderId,
                        principalTable: "OrdersHistories",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_OrderedCarts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderedCartItems",
                columns: table => new
                {
                    OrderedCartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderedCartId = table.Column<int>(type: "int", nullable: false),
                    SetId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedCartItems", x => x.OrderedCartItemId);
                    table.ForeignKey(
                        name: "FK_OrderedCartItems_OrderedCarts_OrderedCartId",
                        column: x => x.OrderedCartId,
                        principalTable: "OrderedCarts",
                        principalColumn: "OrderedCartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderedCartItems_Sets_SetId",
                        column: x => x.SetId,
                        principalTable: "Sets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_SetId",
                table: "CartItems",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CategoryId",
                table: "Offers",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_LEGOSetId",
                table: "Offers",
                column: "LEGOSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_UserId",
                table: "Offers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedCartItems_OrderedCartId",
                table: "OrderedCartItems",
                column: "OrderedCartId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedCartItems_SetId",
                table: "OrderedCartItems",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedCarts_OrdersHistoryOrderId",
                table: "OrderedCarts",
                column: "OrdersHistoryOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedCarts_UserId",
                table: "OrderedCarts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersHistories_UserId",
                table: "OrdersHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerReviews_SellerUserId",
                table: "SellerReviews",
                column: "SellerUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "OrderedCartItems");

            migrationBuilder.DropTable(
                name: "SellerReviews");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "OrderedCarts");

            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.DropTable(
                name: "OrdersHistories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
