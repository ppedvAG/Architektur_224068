using Carshop9000.Model.DomainModel;

namespace Carshop9000.Model.Contracts.Services
{
    public interface ICarService
    {
        void CreateDemoCars();
        Car? GetFastestRedCar();
    }
}
