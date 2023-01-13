using Carshop9000.Model.DomainModel;

namespace Carshop9000.Model.Contracts.Services
{
    public interface IOrderService
    {
        Order CreateNewOrder(Car car, Customer customer);
    }
}
