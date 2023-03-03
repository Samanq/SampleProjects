using AuditTrailSample.Domain.Entities;

namespace AuditTrailSample.Application.Repositories;

public interface IProductRepository
{
    public Product GetById();
    public IEnumerable<Product> GetAll();
    public void Create(Product product);
    public void Edit(long id, Product product);
    public void Delete(long id);
}
