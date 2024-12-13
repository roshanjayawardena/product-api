using AutoMapper;
using MediatR;
using Product.Application.Contracts.Persistence;

namespace Product.Application.Features.Product.Queries.GetAllProducts
{
    public class GetAllVehicleRequestHandler : IRequestHandler<GetAllProductRequest, List<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetAllVehicleRequestHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductRequest request, CancellationToken cancellationToken)
        {
            // query the databaase
            var products = await _productRepository.GetAllAsync(query => query.OrderByDescending(p => p.CreatedDate));

            // convert to dto objects
            var result = _mapper.Map<List<ProductDto>>(products);
            return result;
        }
    }
}
