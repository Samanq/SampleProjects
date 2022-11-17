using ApiValidationSample.Models;
using FluentValidation;

namespace ApiValidationSample.Services;

public class DogToyService : IDogToyService
{
    private static List<DogToy> _dogToys = new List<DogToy>()
    {
        new DogToy
        {
            Name= "Ball",
            Color = "Red",
            HasSound = true
        }
    };

    private readonly IValidator<DogToy> _validator;

    // Injecting the IValidaor
    public DogToyService(IValidator<DogToy> validator)
    {
        _validator = validator;
    }

    public IEnumerable<DogToy> GetAll()
    {
        return _dogToys;
    }

    public async Task Create(DogToy dogToy)
    {
        // throw exception if validation fails.
        await _validator.ValidateAndThrowAsync(dogToy);

        _dogToys.Add(dogToy);
    }
}
