using StrategyPattern.Models;

namespace StrategyPattern.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(string productId);
        Task<List<Product>> GetAllByUserIdAsync(string userId);
        Task<Product> SaveAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
