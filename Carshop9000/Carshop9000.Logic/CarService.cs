using Bogus;
using Carshop9000.Model.Contracts;
using Carshop9000.Model.DomainModel;

namespace Carshop9000.Logic
{
    public class CarService
    {
        public CarService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public Car? GetFastestRedCar()
        {
            return UnitOfWork.GetRepo<Car>()
                             .Query()
                             .Where(x => x.Color == "red")
                             .OrderByDescending(x => x.KW).FirstOrDefault();
        }

        public void CreateDemoCars()
        {
            var faker = new Faker<Car>("de");
            faker.UseSeed(4);
            faker.RuleFor(x => x.Model, x => x.Vehicle.Model());
            faker.RuleFor(x => x.Color, x => x.Commerce.Color());
            faker.RuleFor(x => x.KW, x => x.Random.Int(20, 500));
            faker.RuleFor(x => x.BuildDate, x => x.Date.Past(4));
            faker.RuleFor(x => x.Manufacturer, x => new Manufacturer()
            {
                Name = x.Vehicle.Manufacturer(),
                City = x.Address.City()
            });

            foreach (var car in faker.Generate(20))
            {
                UnitOfWork.GetRepo<Car>().Add(car);
            }

            UnitOfWork.SaveAll();
        }
    }
}