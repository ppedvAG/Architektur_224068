using Carshop9000.Model.Contracts.Repository;
using Carshop9000.Model.Contracts.Services;
using Carshop9000.Model.DomainModel;

namespace Carshop9000.Logic
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork uow;
        private readonly ICarService carService;
        private readonly ICustomerService customerService;

        public OrderService(IUnitOfWork uow, ICarService carService, ICustomerService customerService)
        {
            this.uow = uow;
            this.carService = carService;
            this.customerService = customerService;
        }

        public Order CreateNewOrder(Car car, Customer customer)
        {
            //if (carService.IsCarAvailable(car) && !customerService.IsKnowBadCustomer(customer))
            //{
            //    var order = new Order() { BillingCustomer = customer };
            //}

            throw new NotImplementedException();
        }
    }
}