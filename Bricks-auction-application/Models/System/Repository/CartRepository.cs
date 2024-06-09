using Bricks_auction_application.Models;
using Bricks_auction_application.Models.System.Repository.IRepository;
using Bricks_auction_application.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bricks_auction_application.Models.System.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly BricksAuctionDbContext _db;

        public CartRepository(BricksAuctionDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Cart cart)
        {
            _db.Carts.Update(cart);
        }

        public Cart GetFirstOrDefault(Expression<Func<Cart, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<Cart> query = _db.Carts;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                query = IncludeProperties(query, includeProperties);
            }

            return query.FirstOrDefault();
        }

        public async Task<Cart> GetFirstOrDefaultAsync(Expression<Func<Cart, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<Cart> query = _db.Carts;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                query = IncludeProperties(query, includeProperties);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Cart>> GetAllAsync(Expression<Func<Cart, bool>> filter = null, Func<IQueryable<Cart>, IOrderedQueryable<Cart>> orderBy = null, string includeProperties = null)
        {
            IQueryable<Cart> query = _db.Carts;

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

        public async Task<Cart> GetAsync(int id)
        {
            return await _db.Carts.FindAsync(id);
        }

        private IQueryable<Cart> IncludeProperties(IQueryable<Cart> query, string includeProperties)
        {
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }


        public Cart GetFirstOrDefault(Expression<Func<Cart, bool>> filter)
        {
            IQueryable<Cart> query = _db.Carts;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.FirstOrDefault();
        }

        public async Task<Cart> GetCartByUserIdAsync(string UserId)
        {
            return await _db.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == UserId);
        }
    }
}
