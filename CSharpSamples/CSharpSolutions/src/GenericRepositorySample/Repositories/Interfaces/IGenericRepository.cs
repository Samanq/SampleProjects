namespace GenericRepositorySample.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task Add(T entity);
    Task Delete(long id);
    Task Edit(T entity);
    Task<T?> Get(long id);
    Task<IEnumerable<T>> GetAll();
}