using Autofac;
using Autofac.Core;
using Carshop9000.Data.EfCore;
using Carshop9000.Logic;
using Carshop9000.Model.Contracts.Repository;
using Carshop9000.Model.Contracts.Services;
using Carshop9000.Model.DomainModel;
using DryIoc;
using System.Reflection;

Console.WriteLine("*** Carshop 9000 v0.1 ***");

var conString = "Server=(localdb)\\mssqllocaldb;Database=Carshop9000_Test;Trusted_Connection=true";

//manual injection
//var uow = new EfUnitOfWork(conString);

//DI per Reflection
//var pfath = @"C:\Users\Fred\source\repos\ppedvAG\Architektur_224068\Carshop9000\Carshop9000.Data.EfCore\bin\Debug\net6.0\Carshop9000.Data.EfCore.dll";
//var ass = Assembly.LoadFrom(pfath);
//Type typeMitIRepo = ass.GetTypes().FirstOrDefault(x=>x.GetInterfaces().Contains(typeof(IRepository)));
//IRepository repo = Activator.CreateInstance(typeMitIRepo,conString) as IRepository;

//DI per AutoFac
var builder = new ContainerBuilder();
builder.RegisterType<EfUnitOfWork>().WithParameter("conString", conString).As<IUnitOfWork>();
builder.RegisterType<CarService>().AsImplementedInterfaces();
builder.RegisterType<CustomerService>().AsImplementedInterfaces();
builder.RegisterType<OrderService>().AsImplementedInterfaces();
var container = builder.Build();

//DI per DryIoc
//var container = new DryIoc.Container();
//container.RegisterInstance<IRepository>(new EfRepository(conString));
//container.Register<CarService, CarService>();

var uow = container.Resolve<IUnitOfWork>();
var carService = new CarService(uow);

//var carService = container.Resolve<CarService>();
//carService.CreateDemoCars();

//foreach (var car in uow.GetRepo<Car>().Query().ToList())
foreach (var car in uow.CarRepo.Query().ToList())
{
    Console.WriteLine($"{car.Manufacturer?.Name} {car.Model} {car.Color} {car.Manufacturer?.City}");
    //Console.WriteLine($" {car.Model} {car.Color} ");
}

Console.WriteLine();
var bestCar = carService.GetFastestRedCar();
Console.WriteLine($"Schnellstest rotes Auto: {bestCar.Manufacturer?.Name} {bestCar.Model}");
