namespace ECommerce.DTOs
{
    public class OrderCreateUpdateDto
    {
        public int CustomerId { get; set; }
        public List<OrderItemCreateDto> Items { get; set; } = new List<OrderItemCreateDto>();
    }
}
