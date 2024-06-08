using Bricks_auction_application.Models.Users;

namespace Bricks_auction_application.Models.System.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader obj);
    }
}
