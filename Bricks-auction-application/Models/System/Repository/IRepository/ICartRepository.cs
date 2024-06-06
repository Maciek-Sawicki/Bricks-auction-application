using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bricks_auction_application.Models.Sets;
using Bricks_auction_application.Models.Users;

namespace Bricks_auction_application.Models.System.Repository.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {
        void Delete(Cart cart);
        void Update(Cart cart);
        void Remove(Cart cart);
        Task<Cart> GetFirstOrDefaultAsync(Expression<Func<Cart, bool>> filter = null, string includeProperties = null);
        // Add the following method declaration
        Cart GetFirstOrDefault(Expression<Func<Cart, bool>> filter = null);
        Task<IEnumerable<Cart>> GetAllAsync(Expression<Func<Cart, bool>> filter = null, Func<IQueryable<Cart>, IOrderedQueryable<Cart>> orderBy = null);
        Task<Cart> GetAsync(int id);
    }
}

