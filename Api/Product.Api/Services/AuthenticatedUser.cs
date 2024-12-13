using Product.Application.Contracts.Infastructure.Auth;
using System.Security.Claims;

namespace Product.Api.Services
{
    public class AuthenticatedUser : IAuthenticatedUser
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null || !user.Identity.IsAuthenticated)
                    return null;

                // Replace "sub" with your claim type for UserId
                return _httpContextAccessor.HttpContext.User.FindFirstValue("uid");
            }
        }
    }
}
