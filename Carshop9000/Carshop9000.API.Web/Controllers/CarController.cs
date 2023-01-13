using AutoMapper;
using Carshop9000.API.Web.DTOs;
using Carshop9000.Model.Contracts;
using Carshop9000.Model.DomainModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Carshop9000.API.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        IMapper mapper;

        public CarController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            mapper = new MapperConfiguration(x =>
            {
                x.CreateMap<Car, CarDto>();
                x.CreateMap<CarDto, Car>().ForMember(x => x.Manufacturer, x => x.Ignore());
            }).CreateMapper();
        }

        // GET: api/<CarController>
        [HttpGet]
        public IEnumerable<CarDto> Get()
        {
            var query = _unitOfWork.CarRepo.Query().ToList();
            foreach (var item in query)
            {
                yield return mapper.Map<CarDto>(item);
                //yield return new CarDto()
                //{
                //    Id = item.Id,
                //    Manufacturer = item.Manufacturer?.Name,
                //    Model = item.Model,
                //    Color = item.Color,
                //    KW = item.KW
                //};
            }
        }

        // GET api/<CarController>/5
        [HttpGet("{id}")]
        public CarDto Get(int id)
        {
            var car = _unitOfWork.CarRepo.GetById(id);
            return mapper.Map<CarDto>(car);
        }

        // POST api/<CarController>
        [HttpPost]
        public void Post([FromBody] CarDto newCarDto)
        {
            var car = mapper.Map<Car>(newCarDto);
            _unitOfWork.CarRepo.Add(car);
            _unitOfWork.SaveAll();
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CarDto carDto)
        {
            var car = mapper.Map<Car>(carDto);
            _unitOfWork.CarRepo.Update(car);
            _unitOfWork.SaveAll();
        }

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _unitOfWork.CarRepo.Delete(_unitOfWork.CarRepo.GetById(id));
            _unitOfWork.SaveAll();
        }
    }
}
