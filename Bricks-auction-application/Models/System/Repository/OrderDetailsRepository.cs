using Bricks_auction_application.Models.Offers;
using Bricks_auction_application.Models.System.Repository.IRepository;

namespace Bricks_auction_application.Models.System.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private BricksAuctionDbContext _db;
        public OrderDetailsRepository(BricksAuctionDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(OrderDetails obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }

}
