﻿using Bricks_auction_application.Models.Items;
using Bricks_auction_application.Models.Offers;
using Bricks_auction_application.Models.Sets;
using Bricks_auction_application.Models.Users;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Bricks_auction_application.Models
{
    public class BricksAuctionDbContext : IdentityDbContext<IdentityUser>
    {
        public BricksAuctionDbContext(DbContextOptions<BricksAuctionDbContext> options) : base(options)
        {
        }

        public DbSet<Set> Sets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderedCartItem> OrderedCartItems { get; set; }
        public DbSet<OrderedCart> OrderedCarts { get; set; }
        public DbSet<OrdersHistory> OrdersHistories { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Offer)
                .WithMany()
                .HasForeignKey(c => c.OfferId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderedCartItem>()
                .HasOne(oc => oc.OrderedCart)
                .WithMany(oc => oc.Items)
                .HasForeignKey(oc => oc.OrderedCartId)
                .OnDelete(DeleteBehavior.Restrict);

            // Dodaj inne konfiguracje relacji
            modelBuilder.Entity<Offer>()
               .HasOne(oc => oc.User)
               .WithMany()
               .HasForeignKey(oc => oc.UserId)
               .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }


    }
}