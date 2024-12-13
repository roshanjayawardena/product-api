using FluentValidation;
using Product.Application.Contracts.Persistence;

namespace Product.Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        private IProductRepository _productRepository;
        public CreateProductRequestValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(t => t.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
            RuleFor(t => t.Code).NotNull().NotEmpty().WithMessage("Code cannot be empty.")
                .MustAsync(UniqueProductCode).WithMessage("Product code already exist");
            RuleFor(t => t.Description).NotNull().NotEmpty().WithMessage("Description cannot be empty.");
            RuleFor(t => t.Price).NotNull().WithMessage("Price cannot be empty.")
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");
        }

        private async Task<bool> UniqueProductCode(string code, CancellationToken token)
        {
            return await _productRepository.IsCodeUniqueAsync(code);
        }


    }
}
