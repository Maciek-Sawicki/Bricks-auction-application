using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bricks_auction_application.Models.Users;

namespace Bricks_auction_application.Models.System.Repository.IRepository
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        void Delete(CartItem cartItem);
        void Update(CartItem cartItem);
        CartItem GetFirstOrDefault(Expression<Func<CartItem, bool>> filter = null);
        void Remove(CartItem cartItem);
        Task<CartItem> GetAsync(int id);
        Task<IEnumerable<CartItem>> GetAllAsync(Expression<Func<CartItem, bool>> filter = null, Func<IQueryable<CartItem>, IOrderedQueryable<CartItem>> orderBy = null, string includeProperties = null);
        Task<CartItem> GetFirstOrDefaultAsync(Expression<Func<CartItem, bool>> filter, string includeProperties = null);
    }
}
