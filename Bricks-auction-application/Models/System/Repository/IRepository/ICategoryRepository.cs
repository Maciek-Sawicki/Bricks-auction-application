using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bricks_auction_application.Models.Sets;

namespace Bricks_auction_application.Models.System.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Delete(Category category);
        bool Exists(int id);
        void Update(Category category);
        Category GetFirstOrDefault(Expression<Func<Category, bool>> filter = null);
        void Remove(Category category);
        IEnumerable<Category> GetAllCategories();
        Task<Category> GetAsync(int id);
    }
}
