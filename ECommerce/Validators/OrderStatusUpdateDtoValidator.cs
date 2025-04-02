using ECommerce.DTOs;
using FluentValidation;

namespace ECommerce.Validators
{
    public class OrderStatusUpdateDtoValidator : AbstractValidator<OrderStatusUpdateDto>
    {
        public OrderStatusUpdateDtoValidator()
        {
            RuleFor(o => o.Status).NotEmpty().WithMessage("Status is required")
                                 .Must(BeValidStatus).WithMessage("Invalid order status");
        }

        private bool BeValidStatus(string status)
        {
            return status == "Pending" || status == "Delivered";
        }
    }
}
