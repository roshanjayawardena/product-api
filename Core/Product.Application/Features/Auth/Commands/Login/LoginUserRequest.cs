using MediatR;

namespace Product.Application.Features.Auth.Commands.Login
{
    public class LoginUserRequest : IRequest<LoginDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
