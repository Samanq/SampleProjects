using AuditTrailSample.Application.Repositories;
using AuditTrailSample.Domain.Entities;
using AuditTrailSample.Infrastructure.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace AuditTrailSample.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AuditTrailSampleDb _db;

    public ProductRepository(AuditTrailSampleDb dbContext)
    {
        _db = dbContext;
    }


    public async Task Create(Product product)
    {
        await _db.Products.AddAsync(product);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(Product product)
    {
        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
    }

    public async Task Edit(Product product)
    {
        _db.Entry(product).State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _db.Products.ToListAsync();
    }

    public async Task<Product?> GetById(long id)
    {
        return await _db.Products.FindAsync(id);
    }
}
