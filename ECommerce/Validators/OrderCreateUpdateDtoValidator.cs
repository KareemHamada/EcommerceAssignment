using ECommerce.DTOs;
using FluentValidation;

namespace ECommerce.Validators
{
    public class OrderCreateUpdateDtoValidator : AbstractValidator<OrderCreateUpdateDto>
    {
        public OrderCreateUpdateDtoValidator()
        {
            RuleFor(o => o.CustomerId).NotEmpty().WithMessage("Customer ID is required");
            RuleFor(o => o.Items).NotEmpty().WithMessage("Order must contain at least one item");
            RuleForEach(o => o.Items).SetValidator(new OrderItemCreateDtoValidator());
        }
    }
}
