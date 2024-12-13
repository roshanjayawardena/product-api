using AutoMapper;
using Product.Application.Features.Auth.Commands.Login;
using Product.Application.Features.Auth.Commands.RefreshToken;
using Product.Application.Models.Auth;

namespace Product.Application.Configurations.MappingProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<LoginUserRequest, LoginUser>();              
            CreateMap<RefreshTokenRequest, RefreshTokenDto>();
        }
    }
}
