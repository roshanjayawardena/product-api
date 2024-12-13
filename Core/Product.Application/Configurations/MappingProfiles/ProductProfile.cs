using AutoMapper;
using Product.Application.Features.Product.Commands.CreateProduct;
using Product.Application.Features.Product.Commands.UpdateProduct;
using Product.Application.Features.Product.Queries.GetAllProducts;
using Product.Application.Features.Product.Queries.GetProductDetail;

namespace Product.Application.Configurations.MappingProfiles
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<UpdateProductRequest, Domain.Product>();
            CreateMap<CreateProductRequest, Domain.Product>();
            CreateMap<ProductDto, Domain.Product>().ReverseMap();
            CreateMap<ProductDetailDto, Domain.Product>().ReverseMap(); ;
        }
    }
}
