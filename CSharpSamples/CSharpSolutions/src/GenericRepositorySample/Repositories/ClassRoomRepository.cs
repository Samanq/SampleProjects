namespace GenericRepositorySample.Repositories;

using GenericRepositorySample.Data;
using GenericRepositorySample.Entities;
using GenericRepositorySample.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ClassRoomRepository : GenericRepository<ClassRoom>, IClassRoomRepository
{
    private DataContext _dataContext;

    public ClassRoomRepository(DataContext dataContext) 
        : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ClassRoom?> GetByNumber(int number)
    {
        return await _dataContext.ClassRooms.SingleOrDefaultAsync(t => t.Number == number);
    }
}
