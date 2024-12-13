using MediatR;

namespace Product.Application.Features.Product.Queries.GetAllProducts
{
    public record GetAllProductRequest : IRequest<List<ProductDto>>;


}
