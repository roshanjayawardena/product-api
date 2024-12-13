using Product.Domain.Common;
using Product.Domain.Common.Enums;

namespace Product.Domain
{
    public class Product :BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public RecordStatusEnum Status { get; set; } = RecordStatusEnum.Active;
        public bool IsDeleted { get; set; } = false;
    }
}
