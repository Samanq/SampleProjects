namespace AutoMapperSample.Api;
using AutoMapper;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		// For Simple Mapping
		//CreateMap<Student, StudentDto>();

		// When source and destination have diffrent properties
        CreateMap<Student, StudentDto>()
			.ForMember(dest =>
			dest.FName,
			opt => opt.MapFrom(src => src.FirstName))
			.ForMember(dest =>
			dest.LName,
			opt => opt.MapFrom(src => src.LastName));

        CreateMap<StudentDto, Student>()
			.ForMember(dest => 
			dest.FirstName,
			opt => opt.MapFrom(src => src.FName))
            .ForMember(dest =>
            dest.LastName,
            opt => opt.MapFrom(src => src.LName));
    }
}
