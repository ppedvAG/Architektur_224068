namespace Carshop9000.Model
{
    public class Order : Entity
    {
        public DateTime OrderDate { get; set; }

        public virtual Customer? BillingCustomer { get; set; }
        public virtual Customer? DeliveryCustomer { get; set; }
    }
}