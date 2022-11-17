using ApiValidationSample.Models;

namespace ApiValidationSample.Services;

public interface IDogToyService
{
    IEnumerable<DogToy> GetAll();
    Task Create(DogToy dogToy);
}