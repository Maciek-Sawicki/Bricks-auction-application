using Bricks_auction_application.Models.Offers;
using Bricks_auction_application.Models.Sets;
using Bricks_auction_application.Models.System.Repository;
using Bricks_auction_application.Models.System.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bricks_auction_application.Models.System.Respository
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        private BricksAuctionDbContext _db;

        public OfferRepository(BricksAuctionDbContext db) : base(db)
        {
            _db = db;
        }

        public object Include(Func<object, object> value)
        {
            throw new NotImplementedException();
        }

        public void Update(Offer obj)
        {
            _db.Offers.Update(obj);
        }
        public override IEnumerable<Offer> GetAll(
            Expression<Func<Offer, bool>> filter = null,
            Func<IQueryable<Offer>, IOrderedQueryable<Offer>> orderBy = null,
            string includeProperties = null)
        {
            IQueryable<Offer> query = _db.Offers;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Include properties
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Offer GetFirstOrDefault(Expression<Func<Offer, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<Offer> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            Remove(id);
        }

        public void Remove(Offer entity)
        {
            _db.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Offer> entity)
        {
            _db.RemoveRange(entity);
        }

        public async Task<IEnumerable<Offer>> GetAllAsync(Expression<Func<Offer, bool>> filter = null, Func<IQueryable<Offer>, IOrderedQueryable<Offer>> orderBy = null)
        {
            IQueryable<Offer> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Jeśli potrzebujesz załączyć powiązane właściwości, możesz to zrobić tutaj
            query = query.Include(o => o.User).Include(o => o.LEGOSet);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task<Offer> GetAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
        public IEnumerable<Offer> GetOffersByCategory(int categoryId)
        {
            return _db.Offers.Where(o => o.CategoryId == categoryId).ToList();
        }
    }

}
