using Carshop9000.Model.Contracts;
using Carshop9000.Model.DomainModel;

namespace Carshop9000.Logic
{
    public class CarService
    {
        public CarService(IRepository repository)
        {
            Repository = repository;
        }

        public IRepository Repository { get; }

        public void CreateDemoCars()
        {
            for (int i = 0; i < 10; i++)
            {
                var car = new Car() { Color = "Red", Model = $"BrummBrumm {i:00}" };

                Repository.Add(car);
            }

            Repository.SaveAll();
        }
    }
}