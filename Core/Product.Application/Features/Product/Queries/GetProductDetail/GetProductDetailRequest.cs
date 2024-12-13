using MediatR;

namespace Product.Application.Features.Product.Queries.GetProductDetail
{
    public record GetProductDetailRequest(Guid Id) : IRequest<ProductDetailDto>;
}
