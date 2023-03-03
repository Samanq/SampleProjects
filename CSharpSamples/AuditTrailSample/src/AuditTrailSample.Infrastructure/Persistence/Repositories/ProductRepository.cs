using AuditTrailSample.Application.Repositories;
using AuditTrailSample.Domain.Entities;

namespace AuditTrailSample.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    public void Create(Product product)
    {
        throw new NotImplementedException();
    }

    public void Delete(long id)
    {
        throw new NotImplementedException();
    }

    public void Edit(long id, Product product)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> GetAll()
    {
        throw new NotImplementedException();
    }

    public Product GetById()
    {
        throw new NotImplementedException();
    }
}
