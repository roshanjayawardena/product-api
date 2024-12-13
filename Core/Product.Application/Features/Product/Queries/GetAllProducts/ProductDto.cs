using Product.Domain.Common.Enums;

namespace Product.Application.Features.Product.Queries.GetAllProducts
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public RecordStatusEnum Status { get; set; }
    }
}
