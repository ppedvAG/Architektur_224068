namespace Carshop9000.Model
{
    public class OrderItem : Entity
    {
        public virtual Order? Order { get; set; }
        public virtual Car? Car { get; set; }

        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}