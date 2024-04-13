﻿// <auto-generated />
using System;
using Bricks_auction_application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bricks_auction_application.Migrations
{
    [DbContext(typeof(BricksAuctionDbContext))]
    [Migration("20240402211257_First_Azure_Migration")]
    partial class First_Azure_Migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bricks_auction_application.Models.Items.Set", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int");

                    b.Property<string>("SetId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Sets");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Offers.Offer", b =>
                {
                    b.Property<int>("OfferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OfferId"));

                    b.Property<int>("LEGOSetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OfferEndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OfferId");

                    b.HasIndex("LEGOSetId");

                    b.HasIndex("UserId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.SellerReview", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SellerUserId")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("SellerUserId");

                    b.ToTable("SellerReviews");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Sets.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Users.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Users.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartItemId"));

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.HasKey("CartItemId");

                    b.HasIndex("CartId");

                    b.HasIndex("OfferId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Users.OrderedCart", b =>
                {
                    b.Property<int>("OrderedCartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderedCartId"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrdersHistoryId")
                        .HasColumnType("int");

                    b.HasKey("OrderedCartId");

                    b.HasIndex("OrdersHistoryId");

                    b.ToTable("OrderedCarts");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Users.OrderedCartItem", b =>
                {
                    b.Property<int>("OrderedCartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderedCartItemId"));

                    b.Property<int>("OrderedCartId")
                        .HasColumnType("int");

                    b.Property<int>("OrderedOfferId")
                        .HasColumnType("int");

                    b.HasKey("OrderedCartItemId");

                    b.HasIndex("OrderedCartId");

                    b.HasIndex("OrderedOfferId");

                    b.ToTable("OrderedCartItems");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Users.OrdersHistory", b =>
                {
                    b.Property<int>("OrderHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderHistoryId"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderHistoryId");

                    b.HasIndex("UserId");

                    b.ToTable("OrdersHistories");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Users.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Items.Set", b =>
                {
                    b.HasOne("Bricks_auction_application.Models.Sets.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Offers.Offer", b =>
                {
                    b.HasOne("Bricks_auction_application.Models.Items.Set", "LEGOSet")
                        .WithMany()
                        .HasForeignKey("LEGOSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bricks_auction_application.Models.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LEGOSet");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.SellerReview", b =>
                {
                    b.HasOne("Bricks_auction_application.Models.Users.User", "Seller")
                        .WithMany("SellerReviews")
                        .HasForeignKey("SellerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Users.Cart", b =>
                {
                    b.HasOne("Bricks_auction_application.Models.Users.User", "User")
                        .WithOne("Cart")
                        .HasForeignKey("Bricks_auction_application.Models.Users.Cart", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Users.CartItem", b =>
                {
                    b.HasOne("Bricks_auction_application.Models.Users.Cart", "Cart")
                        .WithMany("Items")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bricks_auction_application.Models.Offers.Offer", "Offer")
                        .WithMany()
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Users.OrderedCart", b =>
                {
                    b.HasOne("Bricks_auction_application.Models.Users.OrdersHistory", "OrdersHistory")
                        .WithMany("OrderedCarts")
                        .HasForeignKey("OrdersHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrdersHistory");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Users.OrderedCartItem", b =>
                {
                    b.HasOne("Bricks_auction_application.Models.Users.OrderedCart", "OrderedCart")
                        .WithMany("Items")
                        .HasForeignKey("OrderedCartId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Bricks_auction_application.Models.Offers.Offer", "Offer")
                        .WithMany()
                        .HasForeignKey("OrderedOfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");

                    b.Navigation("OrderedCart");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Users.OrdersHistory", b =>
                {
                    b.HasOne("Bricks_auction_application.Models.Users.User", "User")
                        .WithMany("OrdersHistory")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Users.Cart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Users.OrderedCart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Users.OrdersHistory", b =>
                {
                    b.Navigation("OrderedCarts");
                });

            modelBuilder.Entity("Bricks_auction_application.Models.Users.User", b =>
                {
                    b.Navigation("Cart")
                        .IsRequired();

                    b.Navigation("OrdersHistory");

                    b.Navigation("SellerReviews");
                });
#pragma warning restore 612, 618
        }
    }
}