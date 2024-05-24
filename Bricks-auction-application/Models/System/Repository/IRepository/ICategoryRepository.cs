using System.Collections.Generic;
using Bricks_auction_application.Models.Sets;

namespace Bricks_auction_application.Models.System.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Delete(Category category);
        bool Exists(int id);
        void Update(Category category);
    }
}
