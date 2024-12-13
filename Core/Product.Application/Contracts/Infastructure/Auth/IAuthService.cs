using Product.Application.Features.Auth.Commands.Login;
using Product.Application.Features.Auth.Commands.RefreshToken;
using Product.Application.Models.Auth;

namespace Product.Application.Contracts.Infastructure.Auth
{
    public interface IAuthService
    {
        Task<LoginDto> Login(LoginUser user);     
        Task<LoginDto> RenewTokens(RefreshTokenDto refreshToken);   
      
    }
}
