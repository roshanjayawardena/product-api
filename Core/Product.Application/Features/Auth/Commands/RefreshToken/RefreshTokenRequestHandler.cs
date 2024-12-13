using AutoMapper;
using MediatR;
using Product.Application.Contracts.Infastructure.Auth;
using Product.Application.Exceptions;
using Product.Application.Features.Auth.Commands.Login;

namespace Product.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, LoginDto>
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public RefreshTokenRequestHandler(IMapper mapper, IAuthService authService)
        {

            _mapper = mapper;
            _authService = authService;
        }
        public async Task<LoginDto> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var refreshTokenModel = _mapper.Map<RefreshTokenDto>(request);
            var tokens = await _authService.RenewTokens(refreshTokenModel);

            if (tokens == null)
            {
                throw new BadRequestException("Invalid Refresh Token");
            }
            return tokens;
        }
    }
}
