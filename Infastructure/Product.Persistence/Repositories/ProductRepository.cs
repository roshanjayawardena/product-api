using Microsoft.EntityFrameworkCore;
using Product.Application.Contracts.Persistence;
using Product.Persistence.Context;

namespace Product.Persistence.Repositories
{

    public class ProductRepository : GenericRepository<Domain.Product>, IProductRepository
    {
        public ProductRepository(ProductDbContext context) : base(context)
        {

        }

        public async Task<bool> IsCodeUniqueAsync(string code)
        {
            return !await _dbSet.AnyAsync(p => p.Code == code && !p.IsDeleted);
        }
    }
}
