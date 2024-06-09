using Bricks_auction_application.Models.Users;
using System.Linq.Expressions;

namespace Bricks_auction_application.Models.System.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        Task<IEnumerable<OrderHeader>> GetAllAsync(Expression<Func<OrderHeader, bool>> filter = null, string includeProperties = null);
        Task<OrderHeader> GetFirstOrDefaultAsync(Expression<Func<OrderHeader, bool>> filter, string includeProperties = null);
        void Update(OrderHeader obj);
    }
}
