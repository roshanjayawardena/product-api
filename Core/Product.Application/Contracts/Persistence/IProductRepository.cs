namespace Product.Application.Contracts.Persistence
{
    public interface IProductRepository : IGenericRepository<Domain.Product>
    {
        Task<bool> IsCodeUniqueAsync(string code);
    }

  
}
