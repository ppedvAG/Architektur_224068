namespace Carshop9000.Model
{
    public class Car : Entity
    {
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int KW { get; set; }
        public DateTime BuildDate { get; set; }

        public virtual Manufacturer? Manufacturer { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

    }
}