using Mapster;
using MapsterSample.Contracts.Students;

namespace MapsterSample.WebApi.Mapping;

public class StudentMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<StudentCreateRequest, StudentShortResponse>()
            .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");
    }
}
