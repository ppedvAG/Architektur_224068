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
    }
}