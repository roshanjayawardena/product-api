using AutoMapper;
using MediatR;
using Product.Application.Contracts.Persistence;

namespace Product.Application.Features.Product.Queries.GetProductDetail
{
    public class GetProductDetailRequestHandler : IRequestHandler<GetProductDetailRequest, ProductDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public GetProductDetailRequestHandler(IMapper mapper, IProductRepository productRepository)
        {

            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<ProductDetailDto> Handle(GetProductDetailRequest request, CancellationToken cancellationToken)
        {
            // query the database
            var vehicle = await _productRepository.GetByIdAsync(request.Id);

            // convert to DTO Object
            var result = _mapper.Map<ProductDetailDto>(vehicle);

            return result;
        }
    }
}
