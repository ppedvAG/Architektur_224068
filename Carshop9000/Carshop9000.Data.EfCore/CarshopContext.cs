using Carshop9000.Model;
using Microsoft.EntityFrameworkCore;

namespace Carshop9000.Data.EfCore
{
    public class CarshopContext : DbContext
    {
        private readonly string conString;

        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        public CarshopContext(string conString)
        {
            this.conString = conString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conString).UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().Property(x => x.Model)
                                      .HasMaxLength(50)
                                      .HasColumnName("CarModel");

            modelBuilder.Entity<Customer>().HasMany(x => x.BillingOrders).WithOne(x => x.BillingCustomer!);
            modelBuilder.Entity<Customer>().HasMany(x => x.DeliveryOrders).WithOne(x => x.DeliveryCustomer!);
        }
    }
}