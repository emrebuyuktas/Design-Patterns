using DecoratorPattern.Models;

namespace DecoratorPattern.Repositories.Decorator;

public class ProductRepositoryLoggingDecorator:BaseProductRepositoryDecorator
{
    private readonly ILogger<ProductRepositoryLoggingDecorator> _logger;
    
    public ProductRepositoryLoggingDecorator(IRepository productRepository, ILogger<ProductRepositoryLoggingDecorator> logger) : base(productRepository)
    {
        _logger = logger;
    }

    public override async Task<List<Product>> GetAll()
    {
        _logger.LogInformation("GetAll");
        return await base.GetAll();
    }

    public override async Task<List<Product>> GetAll(string userId)
    {
        _logger.LogInformation($"GetAll by user Id {userId}");
        return await base.GetAll(userId);
    }

    public override async Task<Product> Save(Product product)
    {
        _logger.LogInformation("Save");
        return await base.Save(product);
    }
    
    public override async Task<Product> Update(Product product)
    {
        _logger.LogInformation("Update");
        await base.Update(product);
        return product;
    }
}