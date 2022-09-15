using GenericRepositorySample.Entities;

namespace GenericRepositorySample.Repositories.Interfaces
{
    public interface IClassRoomRepository : IGenericRepository<ClassRoom>
    {
        Task<ClassRoom?> GetByNumber(int number);
    }
}
