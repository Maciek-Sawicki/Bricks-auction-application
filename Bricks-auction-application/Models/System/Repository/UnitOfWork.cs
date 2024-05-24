using Bricks_auction_application.Models.System.Repository.IRepository;
using Bricks_auction_application.Models.System.Respository;

namespace Bricks_auction_application.Models.System.Repository
{
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
        }

        public ICategoryRepository Category { get; private set; }
        public IOfferRepository Offer { get; private set; }
        public ISetRepository Set { get; private set; } // Dodajemy Set do właściwości
        public IUserRepository User { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
