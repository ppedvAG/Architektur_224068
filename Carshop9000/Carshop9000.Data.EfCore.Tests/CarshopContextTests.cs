using AutoFixture;
using AutoFixture.Kernel;
using Carshop9000.Model;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Carshop9000.Data.EfCore.Tests
{
    public class CarshopContextTests
    {
        readonly string _conString = "Server=(localdb)\\mssqllocaldb;Database=Carshop9000_Test;Trusted_Connection=true";

        [Fact]
        public void Can_create_Db()
        {
            var con = new CarshopContext(_conString);
            con.Database.EnsureDeleted();

            var result = con.Database.EnsureCreated();

            Assert.True(result);
        }

        [Fact]
        public void Can_insert_Car()
        {
            var car = new Car() { Color = "Gelb" };
            var con = new CarshopContext(_conString);
            con.Add(car);

            var result = con.SaveChanges();

            Assert.Equal(1, result);
        }

        [Fact]
        public void Can_read_Car()
        {
            var car = new Car() { Color = $"Gelb_{Guid.NewGuid()}" };
            using (var con = new CarshopContext(_conString))
            {
                con.Add(car);
                con.SaveChanges();
            }

            using (var con = new CarshopContext(_conString))
            {
                var loaded = con.Cars.Find(car.Id);
                Assert.NotNull(loaded);
                Assert.Equal(car.Color, loaded.Color);
            }
        }

        [Fact]
        public void Can_update_Car()
        {
            var car = new Car() { Color = $"Gelb_{Guid.NewGuid()}" };
            var newColor = $"Blau_{Guid.NewGuid()}";
            using (var con = new CarshopContext(_conString))
            {
                con.Add(car);
                con.SaveChanges();
            }

            using (var con = new CarshopContext(_conString))
            {
                var loaded = con.Cars.Find(car.Id);
                loaded.Color = newColor;

                var result = con.SaveChanges();
                Assert.Equal(1, result);
            }

            using (var con = new CarshopContext(_conString))
            {
                var loaded = con.Cars.Find(car.Id);
                Assert.NotNull(loaded);
                Assert.Equal(newColor, loaded.Color);
            }
        }

        [Fact]
        public void Can_delete_Car()
        {
            var car = new Car() { Color = $"Gelb_{Guid.NewGuid()}" };
            using (var con = new CarshopContext(_conString))
            {
                con.Add(car);
                con.SaveChanges();
            }

            using (var con = new CarshopContext(_conString))
            {
                var loaded = con.Cars.Find(car.Id);
                con.Remove(loaded);
                var result = con.SaveChanges();
                Assert.Equal(1, result);
            }

            using (var con = new CarshopContext(_conString))
            {
                var loaded = con.Cars.Find(car.Id);
                Assert.Null(loaded);
            }
        }

        [Fact]
        public void Can_insert_Car_AutoFix()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            fix.Customizations.Add(new PropertyNameOmitter(nameof(Entity.Id)));
            var car = fix.Create<Car>();

            using (var con = new CarshopContext(_conString))
            {
                con.Add(car);
                con.SaveChanges();
            }

            using (var con = new CarshopContext(_conString))
            {
                var loaded = con.Cars.Find(car.Id);
                loaded.Should().BeEquivalentTo(car, x => x.IgnoringCyclicReferences());
            }
        }


        [Fact]
        public void Delete_Order_should_delete_OrderItems()
        {
            var order = new Order();
            OrderItem item1 = new OrderItem() { Price = 4m };
            order.Items.Add(item1);
            OrderItem item2 = new OrderItem() { Price = 12m };
            order.Items.Add(item2);
            using (var con = new CarshopContext(_conString))
            {
                con.Add(order);
                con.SaveChanges().Should().Be(3);
            }

            using (var con = new CarshopContext(_conString))
            {
                var loaded = con.Orders.Find(order.Id);
                con.Remove(loaded);
                con.SaveChanges();
            }

            using (var con = new CarshopContext(_conString))
            {
                con.OrderItems.Find(item1.Id).Should().BeNull();
                con.OrderItems.Find(item2.Id).Should().BeNull();
            }
        }

        [Fact]
        public void Delete_Manufacturer_with_Cars_should_throw()
        {
            var car = new Car();
            var man = new Manufacturer();
            man.Cars.Add(car);

            using (var con = new CarshopContext(_conString))
            {
                con.Add(man);
                con.SaveChanges().Should().Be(2);
            }

            using (var con = new CarshopContext(_conString))
            {
                var loaded = con.Manufacturers.Find(man.Id);
                con.Remove(loaded);
                var act = new Action(() => con.SaveChanges());
                act.Should().Throw<DbUpdateException>();
            }

            using (var con = new CarshopContext(_conString))
            {
                con.Cars.Find(car.Id).Should().NotBeNull();
                con.Manufacturers.Find(man.Id).Should().NotBeNull();
            }
        }





        internal class PropertyNameOmitter : ISpecimenBuilder
        {
            private readonly IEnumerable<string> names;

            internal PropertyNameOmitter(params string[] names)
            {
                this.names = names;
            }

            public object Create(object request, ISpecimenContext context)
            {
                var propInfo = request as PropertyInfo;
                if (propInfo != null && names.Contains(propInfo.Name))
                    return new OmitSpecimen();

                return new NoSpecimen();
            }
        }
    }
}