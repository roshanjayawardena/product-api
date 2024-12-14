using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.Persistence.Configurations
{
    public class ProductDatabaseInitialization : IEntityTypeConfiguration<Domain.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.Product> builder)
        {
            builder.Property(q => q.Id)
              .IsRequired()
              .HasMaxLength(50);
            builder.Property(q => q.Code)
               .IsRequired()
               .HasMaxLength(50);           
            builder.HasIndex(p => p.Code).HasFilter("IsDeleted = 0").IsUnique();
            builder.Property(q => q.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(q => q.Description)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(q => q.Price)
               .IsRequired();
            builder.Property(t => t.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Status).HasConversion<int>(); // Enum to int conversion
            builder.Property(q => q.CreatedDate)
                .IsRequired();
            builder.Property(q => q.ModifiedDate);
               
            builder.Property(q => q.CreatedBy)
                .IsRequired();
            builder.Property(q => q.ModifiedBy);
              
        }
    }
}
