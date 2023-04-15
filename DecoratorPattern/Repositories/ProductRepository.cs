using DecoratorPattern.Models;
using Microsoft.EntityFrameworkCore;

namespace DecoratorPattern.Repositories;

public class ProductRepository : IRepository
{
    private readonly AppIdenitytDbContext _context;

    public ProductRepository(AppIdenitytDbContext context)
    {
        _context = context;
    }

    public async Task<Product> GetById(int id)=> await _context.Products.FindAsync(id);

    public async Task<List<Product>> GetAll()=> await _context.Products.ToListAsync();

    public async Task<List<Product>> GetAll(string userId)=> await _context.Products.Where(x => x.UserId == userId).ToListAsync();

    public async Task Remove(Product product)
    {
        _context.Products.Remove(product);

        await _context.SaveChangesAsync();
    }

    public async Task<Product> Save(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task Update(Product product)
    {
        _context.Products.Update(product);

        await _context.SaveChangesAsync();
    }
}