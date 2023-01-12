using Carshop9000.Data.EfCore;
using Carshop9000.Logic;
using Carshop9000.Model.DomainModel;

Console.WriteLine("*** Carshop 9000 v0.1 ***");

var conString = "Server=(localdb)\\mssqllocaldb;Database=Carshop9000_Test;Trusted_Connection=true";
var repo = new EfRepository(conString);

var carService = new CarService(repo);

carService.CreateDemoCars();

foreach (var car in repo.GetAll<Car>())
{
    Console.WriteLine($"{car.Manufacturer?.Name} {car.Model} {car.Color} {car.Manufacturer?.City}");
}
