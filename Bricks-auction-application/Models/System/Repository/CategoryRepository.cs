using Bricks_auction_application.Models.Sets;
using Bricks_auction_application.Models.System.Repository;
using Bricks_auction_application.Models.System.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bricks_auction_application.Models.System.Respository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private BricksAuctionDbContext _db;

        public CategoryRepository(BricksAuctionDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
