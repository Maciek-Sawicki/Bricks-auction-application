using Bricks_auction_application.Models.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bricks_auction_application.Models.System.Repository.IRepository
{
    public interface IOfferRepository : IRepository<Offer>
    {
        void Update(Offer offer);
        bool Exists(int id);
        Offer GetFirstOrDefault(Expression<Func<Offer, bool>> filter = null, string includeProperties = null);
        Task<Offer> GetFirstOrDefaultAsync(Expression<Func<Offer, bool>> filter = null, string includeProperties = null);
        void Remove(Offer offer);
        Task<IEnumerable<Offer>> GetAllAsync(Expression<Func<Offer, bool>> filter = null, Func<IQueryable<Offer>, IOrderedQueryable<Offer>> orderBy = null);
        Task<Offer> GetAsync(int id);
        IEnumerable<Offer> GetOffersByCategory(int categoryId);
    }
}
