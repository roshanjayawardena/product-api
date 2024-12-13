namespace Product.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
       
    }
}
