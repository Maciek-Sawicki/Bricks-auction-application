namespace Bricks_auction_application.Models.System.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IOfferRepository Offer { get; }
        ISetRepository Set { get; } // Dodajemy Set do interfejsu
        IUserRepository User { get; }
        Task SaveAsync();
        void Save();
    }
}
