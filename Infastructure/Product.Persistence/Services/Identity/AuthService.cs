using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Product.Application.Contracts.Infastructure.Auth;
using Product.Application.Exceptions;
using Product.Application.Features.Auth.Commands.Login;
using Product.Application.Features.Auth.Commands.RefreshToken;
using Product.Application.Models.Auth;
using Product.Persistence.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Product.Persistence.Services.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IConfiguration _config;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IOptions<JwtSettings> options, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtSettings = options.Value;
            _config = config;
        }
        public async Task<LoginDto> Login(LoginUser request)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.Email);
            if (identityUser is null)
            {
                throw new BadRequestException("The Email is incorrect,Please contact administrator.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(identityUser, request.Password, false);

            if (!result.Succeeded)
            {
                throw new BadRequestException("The Password is incorrect,Please contact administrator.");
            }

            var token = await GenerateTokenString(request.Email);
            var refreshToken = GenerateRefreshToken();

            identityUser.RefreshToken = refreshToken;
            identityUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenValidityIn);

            await _userManager.UpdateAsync(identityUser);

            return new LoginDto() { AccessToken = token, RefreshToken = refreshToken };
        }

        private string GenerateRefreshToken()
        {
            var randNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randNumber);
            return Convert.ToBase64String(randNumber);
        }

        private async Task<string> GenerateTokenString(string user)
        {
            var claims = await GetClaims(user);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                signingCredentials: signingCred);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        private async Task<List<Claim>> GetClaims(string userName)
        {

            var user = await _userManager.FindByEmailAsync(userName);

            var claims = new List<Claim>() {

                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name,  user.UserName),
                new Claim("uid", user.Id.ToString())
           };

            claims.AddRange(await _userManager.GetClaimsAsync(user));
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                var idenntityRole = await _roleManager.FindByNameAsync(role);
                claims.AddRange(await _roleManager.GetClaimsAsync(idenntityRole));
            }

            return claims;
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenParameters, out SecurityToken securityToken);

            //if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            //    throw new SecurityTokenException("Invalid token");

            return principal;
        }


        public async Task<LoginDto> RenewTokens(RefreshTokenDto refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(refreshToken.AccessToken);

            if (principal?.Identity?.Name is null)
            {
                throw new BadRequestException("Invalid client request");
            }

            var identityUser = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (principal is null || identityUser is null || identityUser.RefreshToken != refreshToken.RefreshToken || identityUser.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new BadRequestException("Invalid client request");

            var newJwtToken = await GenerateTokenString(identityUser.Email);
            var newRefreshToken = GenerateRefreshToken();
            //_ = int.TryParse(_jwtSettings.RefreshTokenValidityIn., out int RefreshTokenValidityIn);

            identityUser.RefreshToken = newRefreshToken;
            identityUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenValidityIn);

            await _userManager.UpdateAsync(identityUser);

            return new LoginDto
            {
                AccessToken = newJwtToken,
                RefreshToken = newRefreshToken
            };
        }


    }
}
