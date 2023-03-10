namespace Carshop9000.Model.DomainModel
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime Modified { get; set; } = DateTime.Now;
        public DateTime Created { get; set; } = DateTime.Now;
    }
}