using Carshop9000.Model.DomainModel;

namespace Carshop9000.Model.Contracts.Repository
{
    public interface IRepository<T> where T : Entity
    {
        T GetById(int id);
        IQueryable<T> Query();

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
