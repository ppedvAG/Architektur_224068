using Carshop9000.Model.Contracts.Repository;
using Carshop9000.Model.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Carshop9000.Data.EfCore
{
    public class EfOrderRepository : EfRepository<Order>, IOrderRepository
    {
        public EfOrderRepository(CarshopContext conContext) : base(conContext)
        { }

        public IEnumerable<Order> GetOrderOfYear(int year)
        {
            _conContext.Database.ExecuteSqlRaw("exec SP_Year", "2023");
            return null;
        }
    }
}
