using Carshop9000.Model.DomainModel;

namespace Carshop9000.Model.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveAll();

        IRepository<T> GetRepo<T>() where T : Entity;
    }
}
