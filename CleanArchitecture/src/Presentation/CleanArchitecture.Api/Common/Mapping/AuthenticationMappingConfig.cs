using CleanArchitecture.Application.Authentication.Commands.Register;
using CleanArchitecture.Application.Authentication.Common;
using CleanArchitecture.Application.Authentication.Queries.Login;
using CleanArchitecture.Contracts.Authentication;
using Mapster;

namespace CleanArchitecture.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User);
    }
}
