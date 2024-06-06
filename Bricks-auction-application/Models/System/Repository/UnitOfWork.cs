﻿using Bricks_auction_application.Models.System.Repository.IRepository;
using Bricks_auction_application.Models.System.Repository;
using Bricks_auction_application.Models.System.Respository;
using Bricks_auction_application.Models;

public class UnitOfWork : IUnitOfWork
{
    private readonly BricksAuctionDbContext _db;

    public UnitOfWork(BricksAuctionDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        Offer = new OfferRepository(_db);
        Set = new SetRepository(_db);
        User = new UserRepository(_db);
        Cart = new CartRepository(_db);
        CartItem = new CartItemRepository(_db);
    }

    public ICategoryRepository Category { get; private set; }
    public IOfferRepository Offer { get; private set; }
    public ISetRepository Set { get; private set; } // Dodajemy Set do właściwości
    public IUserRepository User { get; private set; }
    public ICartRepository Cart { get; private set; }
    public ICartItemRepository CartItem { get; private set; } // Poprawiono typ na ICartItemRepository


    public void Save()
    {
        _db.SaveChanges();
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }

    public void Dispose()
    {
        _db.Dispose();
    }

}
