namespace ECommerce.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}
