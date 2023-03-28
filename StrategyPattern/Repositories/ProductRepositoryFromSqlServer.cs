using StrategyPattern.Models;

namespace StrategyPattern.Repositories
{
    public class ProductRepositoryFromSqlServer : IProductRepository
    {
        private readonly AppIdenitytDbContext _context;

        public ProductRepositoryFromSqlServer(AppIdenitytDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllByUserIdAsync(string userId)=> _context.Products.Where(x=>x.UserId==userId).ToList();

        public async Task<Product> GetByIdAsync(string productId) => await _context.Products.FindAsync(productId);

        public async Task<Product> SaveAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
