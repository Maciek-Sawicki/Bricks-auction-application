namespace Bricks_auction_application.Models.System.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IOfferRepository Offer { get; }
        ISetRepository Set { get; } // Dodajemy Set do interfejsu
        IUserRepository User { get; }
        void Save();
    }
}
