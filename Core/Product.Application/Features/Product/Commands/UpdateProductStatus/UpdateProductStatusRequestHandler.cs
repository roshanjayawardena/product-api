using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Contracts.Persistence;
using Product.Application.Exceptions;

namespace Product.Application.Features.Product.Commands.UpdateProductStatus
{
    public class UpdateProductStatusRequestHandler : IRequestHandler<UpdateProductStatusRequest, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<UpdateProductStatusRequest> _logger;
        public UpdateProductStatusRequestHandler(IProductRepository productRepository, ILogger<UpdateProductStatusRequest> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
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

            _logger.LogInformation($"status of {request.Id} has changed");

            return true;
        }
    }
}
