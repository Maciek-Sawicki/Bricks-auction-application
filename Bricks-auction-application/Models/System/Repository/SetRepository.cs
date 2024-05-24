using Bricks_auction_application.Models.Items;
using Bricks_auction_application.Models.Sets;
using Bricks_auction_application.Models.System.Repository;
using Bricks_auction_application.Models.System.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bricks_auction_application.Models.System.Respository
{
    public class SetRepository : Repository<Set>, ISetRepository
    {
        private readonly BricksAuctionDbContext _db;

        public SetRepository(BricksAuctionDbContext db) : base(db)
        {
            _db = db;
        }
        public Set GetFirstOrDefault(Expression<Func<Set, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<Set> query = _db.Sets;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Include properties
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();
        }

        public void Update(Set obj)
        {
            var dbObj = _db.Sets.FirstOrDefault(s => s.Id == obj.Id);
            if (dbObj != null)
            {
                dbObj.Name = obj.Name;
                dbObj.CategoryId = obj.CategoryId;
                dbObj.Price = obj.Price;
                dbObj.Description = obj.Description;
                dbObj.ImagePath = obj.ImagePath;
                // Add other properties to update as needed
                _db.Sets.Update(dbObj);
            }
        }
    }
}
