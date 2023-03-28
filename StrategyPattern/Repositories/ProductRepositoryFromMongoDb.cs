using MongoDB.Driver;
using StrategyPattern.Models;

namespace StrategyPattern.Repositories
{
    public class ProductRepositoryFromMongoDb : IProductRepository
    {

        private readonly IMongoCollection<Product> _products;

        public ProductRepositoryFromMongoDb(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            var client=new MongoClient(connectionString);
            var database = client.GetDatabase("ProductDb");
            _products = database.GetCollection<Product>("Products");
        }

        public async Task DeleteAsync(Product product)
        {
            await _products.FindOneAndDeleteAsync(x=>x.Id == product.Id);
        }

        public async Task<List<Product>> GetAllByUserIdAsync(string userId)=> await _products.Find(x=>x.UserId==userId).ToListAsync();

        public async Task<Product> GetByIdAsync(string productId) => await _products.Find(x=>x.Id==productId).FirstOrDefaultAsync();

        public async Task<Product> SaveAsync(Product product)
        {
            await _products.InsertOneAsync(product);
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            await _products.FindOneAndReplaceAsync(x => x.Id == product.Id, product);
            return product;
        }
    }
}
