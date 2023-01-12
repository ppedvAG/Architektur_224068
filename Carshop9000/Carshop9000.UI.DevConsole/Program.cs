using Autofac;
using Autofac.Core;
using Carshop9000.Data.EfCore;
using Carshop9000.Logic;
using Carshop9000.Model.Contracts;
using Carshop9000.Model.DomainModel;
using DryIoc;
using System.Reflection;

Console.WriteLine("*** Carshop 9000 v0.1 ***");

var conString = "Server=(localdb)\\mssqllocaldb;Database=Carshop9000_Test;Trusted_Connection=true";

//manual injection
//var repo = new EfRepository(conString);

//DI per Reflection
//var pfath = @"C:\Users\Fred\source\repos\ppedvAG\Architektur_224068\Carshop9000\Carshop9000.Data.EfCore\bin\Debug\net6.0\Carshop9000.Data.EfCore.dll";
//var ass = Assembly.LoadFrom(pfath);
//Type typeMitIRepo = ass.GetTypes().FirstOrDefault(x=>x.GetInterfaces().Contains(typeof(IRepository)));
//IRepository repo = Activator.CreateInstance(typeMitIRepo,conString) as IRepository;

//DI per AutoFac
//var builder = new ContainerBuilder();
//builder.RegisterType<EfRepository>().WithParameter("conString", conString).As<IRepository>();
//builder.RegisterType<CarService>().AsSelf();
//var container = builder.Build();

//DI per DryIoc
var container = new DryIoc.Container();
container.RegisterInstance<IRepository>(new EfRepository(conString));
container.Register<CarService, CarService>();

var repo = container.Resolve<IRepository>();
//var carService = new CarService(repo);

var carService = container.Resolve<CarService>();
carService.CreateDemoCars();

foreach (var car in repo.GetAll<Car>())
{
    Console.WriteLine($"{car.Manufacturer?.Name} {car.Model} {car.Color} {car.Manufacturer?.City}");
}

Console.WriteLine();
var bestCar = carService.GetFastestRedCar();
Console.WriteLine($"Schnellstest rotes Auto: {bestCar.Manufacturer?.Name} {bestCar.Model}");