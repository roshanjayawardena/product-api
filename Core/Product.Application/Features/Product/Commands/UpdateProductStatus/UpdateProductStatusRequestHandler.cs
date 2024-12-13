using MediatR;
using Product.Application.Contracts.Persistence;
using Product.Application.Exceptions;

namespace Product.Application.Features.Product.Commands.UpdateProductStatus
{
    public class UpdateProductStatusRequestHandler : IRequestHandler<UpdateProductStatusRequest, bool>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductStatusRequestHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(UpdateProductStatusRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {request.Id} not found.","Not found");
            }

            product.Status = request.Status;
            await _productRepository.UpdateAsync(product);
            return true;
        }
    }
}
