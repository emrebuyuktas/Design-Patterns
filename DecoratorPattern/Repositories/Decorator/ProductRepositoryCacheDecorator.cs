using DecoratorPattern.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DecoratorPattern.Repositories.Decorator;

public class ProductRepositoryCacheDecorator : BaseProductRepositoryDecorator
{
    private readonly IMemoryCache _memoryCache;
    
    public ProductRepositoryCacheDecorator(IRepository productRepository, IMemoryCache memoryCache) : base(productRepository)
    {
        _memoryCache = memoryCache;
    }

    public override async Task<List<Product>> GetAll()
    {
        if(_memoryCache.TryGetValue("products", out List<Product> products))
            return products;
        await UpdateCache();
        return _memoryCache.Get<List<Product>>("products");
    }

    public override async Task<List<Product>> GetAll(string userId)
    {
        var products = await GetAll();
        return products.Where(x => x.UserId == userId).ToList();
    }


    public override async Task<Product> Save(Product product)
    {
        await base.Save(product);
        await UpdateCache();
        return product;
    }
    
    public override async Task<Product> Update(Product product)
    {
        await base.Update(product);
        await UpdateCache();
        return product;
    }

    private async Task UpdateCache()
    {
        _memoryCache.Set("products", await base.GetAll());
    }
}