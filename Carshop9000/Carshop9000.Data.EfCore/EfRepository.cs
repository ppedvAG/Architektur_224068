using Carshop9000.Model.Contracts;
using Carshop9000.Model.DomainModel;

namespace Carshop9000.Data.EfCore
{
    public class EfRepository : IRepository
    {
        protected CarshopContext _conContext;

        public EfRepository(string conString)
        {
            _conContext = new CarshopContext(conString);
        }

        public void Add<T>(T entity) where T : Entity
        {
            _conContext.Set<T>().Add(entity);
            //if (typeof(T) == typeof(Car))
            //    _conContext.Cars.Add(entity as Car);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            _conContext.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return _conContext.Set<T>().ToList();
        }

        public T GetById<T>(int id) where T : Entity
        {
            return _conContext.Set<T>().Find(id);
        }

        public void SaveAll()
        {
            _conContext.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            _conContext.Set<T>().Update(entity);
        }
    }
}
