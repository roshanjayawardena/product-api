using AutoMapper;
using MediatR;
using Product.Application.Contracts.Infastructure.Auth;
using Product.Application.Models.Auth;

namespace Product.Application.Features.Auth.Commands.Login
{
    public class LoginUserRequestHandler : IRequestHandler<LoginUserRequest, LoginDto>
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public LoginUserRequestHandler(IMapper mapper, IAuthService authService)
        {
            _mapper = mapper;
            _authService = authService;
        }


        public async Task<LoginDto> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var loginModel = _mapper.Map<LoginUser>(request);
            return await _authService.Login(loginModel);
        }
    }
}
