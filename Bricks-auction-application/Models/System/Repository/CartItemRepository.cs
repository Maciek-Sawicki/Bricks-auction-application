using System;
using System.Linq;
using System.Linq.Expressions;
using Bricks_auction_application.Models.System.Repository.IRepository;
using Bricks_auction_application.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Bricks_auction_application.Models.System.Repository
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        private readonly BricksAuctionDbContext _db;

        public CartItemRepository(BricksAuctionDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CartItem cartItem)
        {
            _db.CartItems.Update(cartItem);
        }

        public CartItem GetFirstOrDefault(Expression<Func<CartItem, bool>> filter = null)
        {
            return _db.CartItems.FirstOrDefault(filter);
        }
        public async Task<CartItem> GetAsync(int id)
        {
            return await _db.CartItems.FindAsync(id);
        }

        public async Task<IEnumerable<CartItem>> GetAllAsync(Expression<Func<CartItem, bool>> filter = null, Func<IQueryable<CartItem>, IOrderedQueryable<CartItem>> orderBy = null, string includeProperties = null)
        {
            IQueryable<CartItem> query = _db.CartItems;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includeProperties != null)
            {
                query = IncludeProperties(query, includeProperties);
            }

            return await query.ToListAsync();
        }

        private IQueryable<CartItem> IncludeProperties(IQueryable<CartItem> query, string includeProperties)
        {
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

    }
}
