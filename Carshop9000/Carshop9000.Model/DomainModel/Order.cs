namespace Carshop9000.Model.DomainModel
{
    public class Order : Entity
    {
        public DateTime OrderDate { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public virtual Customer? BillingCustomer { get; set; }
        public virtual Customer? DeliveryCustomer { get; set; }
    }
}