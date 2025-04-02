using ECommerce.DTOs;
using FluentValidation;

namespace ECommerce.Validators
{
    public class OrderItemCreateDtoValidator : AbstractValidator<OrderItemCreateDto>
    {
        public OrderItemCreateDtoValidator()
        {
            RuleFor(oi => oi.ProductId).NotEmpty().WithMessage("Product ID is required");
            RuleFor(oi => oi.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}
