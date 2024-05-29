using Bricks_auction_application.Models.Offers;
using Bricks_auction_application.Models.Sets;
using Bricks_auction_application.Models.System.Repository;
using Bricks_auction_application.Models.System.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bricks_auction_application.Models.System.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly BricksAuctionDbContext _db;

        public CategoryRepository(BricksAuctionDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(int id)
        {
            return _db.Categories.Any(c => c.Id == id);
        }

        public void Remove(int id)
        {
            var entityToRemove = _db.Categories.Find(id);
            Remove(entityToRemove);
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _db.Categories.ToList();
        }

        public Category GetFirstOrDefault(Expression<Func<Category, bool>> filter = null)
        {
            IQueryable<Category> query = _db.Categories;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.FirstOrDefault();
        }

        public async Task<Category> GetAsync(int id)
        {
            return await _db.Categories.FindAsync(id);
        }
    }
}
