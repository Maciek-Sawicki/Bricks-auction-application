using Bricks_auction_application.Models.Offers;

namespace Bricks_auction_application.Models.System.Repository.IRepository
{
    public interface IOrderDetailsRepository : IRepository<OrderDetails>
    {
        void Update(OrderDetails obj);
    }
}
