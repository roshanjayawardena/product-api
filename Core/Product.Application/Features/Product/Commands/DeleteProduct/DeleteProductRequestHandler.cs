using MediatR;
using Product.Application.Contracts.Persistence;
using Product.Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace Product.Application.Features.Product.Commands.DeleteProduct
{
    public class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest, Unit>
    {
        private IProductRepository _productRepository;
        private readonly ILogger<DeleteProductRequest> _logger;
        public DeleteProductRequestHandler(IProductRepository productRepository, ILogger<DeleteProductRequest> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            // retrieve domain entity
            var item = await _productRepository.GetByIdAsync(request.Id);

            // verify the item is exist
            if (item is null)
            {
                throw new NotFoundException(nameof(item), request.Id.ToString());
            }

            item.IsDeleted = true;
            // remove the item from database
            await _productRepository.UpdateAsync(item);

            _logger.LogInformation($"{request.Id} has deleted");
           
            return Unit.Value;
        }
    }
}
