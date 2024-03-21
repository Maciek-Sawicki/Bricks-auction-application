using Bricks_auction_application.Models.Items;
using Bricks_auction_application.Models.Offers;
using Bricks_auction_application.Models.Sets;
using Bricks_auction_application.Models.Users;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace Bricks_auction_application.Models
{
    public class BricksAuctionDbContext : DbContext
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
        //public DbSet<UserRating> UserRatings { get; set; }
        //public DbSet<UserConnectRating> UserConnectRatings { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        //    {
        //        relationship.DeleteBehavior = DeleteBehavior.Restrict;
        //    }
        //
        //}
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.RatingsGiven)
                .WithOne(r => r.RatedUser)
                .HasForeignKey(r => r.RatedUserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.RatingsReceived)
                .WithOne(r => r.RatingUser)
                .HasForeignKey(r => r.RatingUserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.OrdersHistory)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);
        }
        

        using Bricks_auction_application.Models.Items;
using Bricks_auction_application.Models.Offers;
using Bricks_auction_application.Models.Sets;
using Bricks_auction_application.Models.Users;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bricks_auction_application.Models
{
    public class BricksAuctionDbContext : DbContext
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

        // public DbSet<UserAverageRating> UserAverageRatings { get; set; }
        public DbSet<UserConnectRating> UserConnectRatings { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Dodaj niestandardowe konfiguracje modelu, jeśli są potrzebne
        }
    }
}
        */

    }
}