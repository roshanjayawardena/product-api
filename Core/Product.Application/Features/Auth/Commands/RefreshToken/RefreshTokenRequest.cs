using MediatR;
using Product.Application.Features.Auth.Commands.Login;

namespace Product.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenRequest : IRequest<LoginDto>
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
