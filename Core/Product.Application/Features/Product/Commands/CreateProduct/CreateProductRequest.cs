using MediatR;

namespace Product.Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductRequest :IRequest<Guid>
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}
