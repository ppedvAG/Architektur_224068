using Carshop9000.Model.Contracts.Repository;
using Carshop9000.Model.DomainModel;

namespace Carshop9000.Data.EfCore
{
    public class EfRepository<T> : IRepository<T> where T : Entity
    {
        protected CarshopContext _conContext;

        public EfRepository(CarshopContext conContext)
        {
            _conContext = conContext;
        }

        public void Add(T entity)
        {
            _conContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _conContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> Query()
        {
            return _conContext.Set<T>();
        }

        public T GetById(int id)
        {
            return _conContext.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            //var loaded = GetById(entity.Id);
            //if (loaded != null)
            //    con.Entry(loaded).CurrentValues.SetValues(entity);
            _conContext.Set<T>().Update(entity);
        }
    }
}
