using Carshop9000.Model.DomainModel;

namespace Carshop9000.Model.Contracts.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetOrderOfYear(int year);
    }
}
