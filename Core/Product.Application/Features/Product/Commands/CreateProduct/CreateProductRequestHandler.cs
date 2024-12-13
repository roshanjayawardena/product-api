using AutoMapper;
using MediatR;
using Product.Application.Contracts.Persistence;
using Product.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Features.Product.Commands.CreateProduct
{
 
    public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public CreateProductRequestHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
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
            // return value
            return itemtoAdd.Id;
        }
    }
}
