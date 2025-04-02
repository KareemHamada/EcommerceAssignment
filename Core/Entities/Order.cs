

namespace Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending";
        public double TotalPrice { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
