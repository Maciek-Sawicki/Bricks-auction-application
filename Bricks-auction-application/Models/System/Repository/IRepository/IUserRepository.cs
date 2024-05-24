using Bricks_auction_application.Models.Users;

namespace Bricks_auction_application.Models.System.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User obj);
    }
}
