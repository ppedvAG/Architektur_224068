using Carshop9000.Model.DomainModel;

namespace Carshop9000.Model.Contracts.Services
{
    public interface ICustomerService
    {
        Customer CreateNewCustomer(string lastName);
    }
}
