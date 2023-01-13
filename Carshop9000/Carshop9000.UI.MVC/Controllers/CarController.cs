using Carshop9000.Model.Contracts.Repository;
using Carshop9000.Model.DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carshop9000.UI.MVC.Controllers
{
    public class CarController : Controller
    {
        private readonly IUnitOfWork _uow;

        public CarController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: CarController
        public ActionResult Index()
        {
            //return View(_uow.CarRepo.Query().ToList());
            return View(_uow.GetRepo<Car>().Query().ToList());
        }

        // GET: CarController/Details/5
        public ActionResult Details(int id)
        {
            return View(_uow.GetRepo<Car>().GetById(id));
        }

        // GET: CarController/Create
        public ActionResult Create()
        {
            return View(new Car() { KW = 100, Color = "gelb", Model = "NEU" });
        }

        // POST: CarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car)
        {
            try
            {
                _uow.GetRepo<Car>().Add(car);
                _uow.SaveAll();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_uow.GetRepo<Car>().GetById(id));
        }

        // POST: CarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Car car)
        {
            try
            {
                _uow.GetRepo<Car>().Update(car);
                _uow.SaveAll();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_uow.GetRepo<Car>().GetById(id));
        }

        // POST: CarController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Car car)
        {
            try
            {
                var loaded = _uow.GetRepo<Car>().GetById(id);
                _uow.GetRepo<Car>().Delete(loaded);
                _uow.SaveAll();

                //_uow.GetRepo<Car>().Delete(car);
                //_uow.SaveAll();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
