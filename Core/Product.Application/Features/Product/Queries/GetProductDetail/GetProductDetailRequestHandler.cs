using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Contracts.Persistence;

namespace Product.Application.Features.Product.Queries.GetProductDetail
{
    public class GetProductDetailRequestHandler : IRequestHandler<GetProductDetailRequest, ProductDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetProductDetailRequest> _logger;

        public GetProductDetailRequestHandler(IMapper mapper, IProductRepository productRepository, ILogger<GetProductDetailRequest> logger)
        {

            _mapper = mapper;
            _productRepository = productRepository;
            _logger = logger;
        }
        public async Task<ProductDetailDto> Handle(GetProductDetailRequest request, CancellationToken cancellationToken)
        {
            // query the database
            var vehicle = await _productRepository.GetByIdAsync(request.Id);

            // convert to DTO Object
            var result = _mapper.Map<ProductDetailDto>(vehicle);

            _logger.LogInformation("Get product detail");

            return result;
        }
    }
}
