using AuditTrailSample.Domain.Entities;

namespace AuditTrailSample.Application.Repositories;

public interface IProductRepository
{
    public Task<Product?> GetById(long id);
    public Task<IEnumerable<Product>> GetAll();
    public Task Create(Product product);
    public Task Edit(Product product);
    public Task Delete(Product product);
}
