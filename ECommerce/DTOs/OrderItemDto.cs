namespace ECommerce.DTOs
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}
