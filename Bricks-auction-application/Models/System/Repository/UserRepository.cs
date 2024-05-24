using Bricks_auction_application.Models.Users;
using Bricks_auction_application.Models.System.Repository;
using Bricks_auction_application.Models.System.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bricks_auction_application.Models.System.Respository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly BricksAuctionDbContext _db;

        public UserRepository(BricksAuctionDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(User obj)
        {
            var dbObj = _db.Users.FirstOrDefault(u => u.UserId == obj.UserId);
            if (dbObj != null)
            {
                dbObj.Email = obj.Email;
                dbObj.Password = obj.Password;
                dbObj.FirstName = obj.FirstName;
                dbObj.LastName = obj.LastName;
                // Add other properties to update as needed
                _db.Users.Update(dbObj);
            }
        }
    }
}
