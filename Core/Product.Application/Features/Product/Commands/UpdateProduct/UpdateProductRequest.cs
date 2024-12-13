using MediatR;

namespace Product.Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductRequest : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}
