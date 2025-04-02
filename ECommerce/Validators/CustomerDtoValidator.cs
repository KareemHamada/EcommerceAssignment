using ECommerce.DTOs;
using FluentValidation;

namespace ECommerce.Validators
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email is required")
                                .EmailAddress().WithMessage("Invalid email format");
        }
    }
}
