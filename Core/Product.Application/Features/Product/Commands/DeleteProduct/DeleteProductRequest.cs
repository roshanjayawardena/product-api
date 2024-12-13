using MediatR;

namespace Product.Application.Features.Product.Commands.DeleteProduct
{
    public record DeleteProductRequest(Guid Id) : IRequest<Unit>;
}
