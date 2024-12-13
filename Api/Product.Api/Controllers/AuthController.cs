using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Auth.Commands.Login;
using Product.Application.Features.Auth.Commands.RefreshToken;

namespace Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody]LoginUserRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<bool>> RefreshToken([FromBody]RefreshTokenRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
