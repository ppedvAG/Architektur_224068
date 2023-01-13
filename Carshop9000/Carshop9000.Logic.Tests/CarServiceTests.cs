using Carshop9000.Model.Contracts.Repository;
using Carshop9000.Model.DomainModel;
using FluentAssertions;
using Moq;

namespace Carshop9000.Logic.Tests
{
    public class CarServiceTests
    {
        [Fact]
        public void GetFastestRedCar_empty_repo_returns_null()
        {
            var repoMock = new Mock<IRepository<Car>>();
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x=>x.GetRepo<Car>()).Returns(repoMock.Object);
            var cs = new CarService(uowMock.Object);

            cs.GetFastestRedCar().Should().BeNull();
        }

        [Fact]
        public void GetFastestRedCar_3_red_cars_the_seconds_is_fastest()
        {
            var c1 = new Car() { Color = "red", KW = 50 };
            var c2 = new Car() { Color = "red", KW = 250 };
            var c3 = new Car() { Color = "red", KW = 150 };
            var repoMock = new Mock<IRepository<Car>>();
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.GetRepo<Car>()).Returns(repoMock.Object);
            repoMock.Setup(x => x.Query()).Returns(() => new[] { c1, c2, c3 }.AsQueryable());

            var cs = new CarService(uowMock.Object);

            cs.GetFastestRedCar().Should().Be(c2);
        }

        [Fact]
        public void GetFastestRedCar_2_cars_the_red_is_fastest()
        {
            var c1 = new Car() { Color = "red", KW = 50 };
            var c2 = new Car() { Color = "blue", KW = 250 };

            var repoMock = new Mock<IRepository<Car>>();
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.GetRepo<Car>()).Returns(repoMock.Object);
            repoMock.Setup(x => x.Query()).Returns(() => new[] { c1, c2 }.AsQueryable());

            var cs = new CarService(uowMock.Object);

            cs.GetFastestRedCar().Should().Be(c1);
        }


    }
}