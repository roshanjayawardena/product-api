using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Contracts.Persistence;
using Product.Application.Exceptions;

namespace Product.Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductRequest> _logger;
        public CreateProductRequestHandler(IProductRepository productRepository, IMapper mapper, ILogger<CreateProductRequest> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Guid> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            // validating incoming data
            var validator = new CreateProductRequestValidator(_productRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToDictionary());
            }

            // convert to domain entity
            var itemtoAdd = _mapper.Map<Domain.Product>(request);

            //add to database
            await _productRepository.CreateAsync(itemtoAdd);

            _logger.LogInformation($"{itemtoAdd.Id} has created");
            // return value
            return itemtoAdd.Id;
        }
    }
}
