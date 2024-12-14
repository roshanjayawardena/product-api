using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Contracts.Persistence;
using Product.Application.Exceptions;

namespace Product.Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductRequest> _logger;

        public UpdateProductRequestHandler(IProductRepository productRepository, IMapper mapper, ILogger<UpdateProductRequest> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            // validating fields
            var validator = new UpdateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {

                throw new ValidationException(validationResult.ToDictionary());
            }

            // get the item from the database
            var itemToEdit = await _productRepository.GetByIdAsync(request.Id);

            if (itemToEdit is null)
            {
                throw new NotFoundException(nameof(itemToEdit), request.Id.ToString());
            }

            // map the data
            _mapper.Map(request, itemToEdit);

            // update the database
            await _productRepository.UpdateAsync(itemToEdit);

            _logger.LogInformation($"{request.Id} has updated");

            return Unit.Value;

        }
    }
}
