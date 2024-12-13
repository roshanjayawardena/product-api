using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Product.Application.Contracts.Infastructure.Auth;
using Product.Domain.Common;
using Product.Persistence.Models;

namespace Product.Persistence.Context
{
    public class ProductDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private readonly IAuthenticatedUser _currentUser;
        public ProductDbContext(DbContextOptions<ProductDbContext> options, IAuthenticatedUser currentUser) : base(options)
        {
            _currentUser = currentUser;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
            // Global filter for active records
            modelBuilder.Entity<Domain.Product>()
                .HasQueryFilter(p => !p.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = Guid.Parse(_currentUser.UserId);
                        entry.Entity.ModifiedBy = Guid.Parse(_currentUser.UserId);                     
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.ModifiedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = Guid.Parse(_currentUser.UserId);                        
                        entry.Entity.ModifiedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<Domain.Product> Products { get; set; }
    }
}
