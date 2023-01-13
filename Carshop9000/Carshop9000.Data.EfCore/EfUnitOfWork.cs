using Carshop9000.Model.Contracts;
using Carshop9000.Model.DomainModel;

namespace Carshop9000.Data.EfCore
{
    public class EfUnitOfWork : IUnitOfWork
    {
        CarshopContext _conContext;
        public EfUnitOfWork(string conString)
        {
            _conContext = new CarshopContext(conString);
        }

        public IOrderRepository OrderRepo => throw new NotImplementedException();

        public IRepository<Car> CarRepo => new EfRepository<Car>(_conContext);

        public void Dispose()
        {
            _conContext.Dispose();
        }

        public IRepository<T> GetRepo<T>() where T : Entity
        {
            return new EfRepository<T>(_conContext);
        }

        public void SaveAll()
        {
            _conContext.SaveChanges();
        }
    }
}
