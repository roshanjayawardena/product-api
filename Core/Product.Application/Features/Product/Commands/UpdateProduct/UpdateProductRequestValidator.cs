using FluentValidation;

namespace Product.Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(t => t.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");          
            RuleFor(t => t.Description).NotNull().NotEmpty().WithMessage("Description cannot be empty.");
            RuleFor(t => t.Price).NotNull().WithMessage("Price cannot be empty.")
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");
        }
    }
}
