using Bricks_auction_application.Models.Offers;
using Bricks_auction_application.Models.System.Repository.IRepository;
using Bricks_auction_application.Models.Users;
namespace Bricks_auction_application.Models.System.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private BricksAuctionDbContext _db;
        public OrderHeaderRepository(BricksAuctionDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(OrderHeader obj) {
            _db.OrderHeaders.Update( obj );
        }
    }
}
