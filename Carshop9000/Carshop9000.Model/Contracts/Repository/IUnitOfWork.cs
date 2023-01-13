using Carshop9000.Model.DomainModel;

namespace Carshop9000.Model.Contracts.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveAll();

        IRepository<T> GetRepo<T>() where T : Entity;

        IOrderRepository OrderRepo { get; }
        IRepository<Car> CarRepo { get; }
    }
}
