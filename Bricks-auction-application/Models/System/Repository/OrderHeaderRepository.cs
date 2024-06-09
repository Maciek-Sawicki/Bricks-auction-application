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
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly BricksAuctionDbContext _db;

        public OrderHeaderRepository(BricksAuctionDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<OrderHeader>> GetAllAsync(Expression<Func<OrderHeader, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<OrderHeader> query = _db.OrderHeaders;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<OrderHeader> GetFirstOrDefaultAsync(Expression<Func<OrderHeader, bool>> filter, string includeProperties = null)
        {
            IQueryable<OrderHeader> query = _db.OrderHeaders;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }
    }
}
