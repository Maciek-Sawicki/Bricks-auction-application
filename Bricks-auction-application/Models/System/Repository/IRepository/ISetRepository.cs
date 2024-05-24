using Bricks_auction_application.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bricks_auction_application.Models.System.Repository.IRepository
{
    public interface ISetRepository : IRepository<Set>
    {
        Set GetFirstOrDefault(Expression<Func<Set, bool>> filter = null, string includeProperties = null);
    }
}
