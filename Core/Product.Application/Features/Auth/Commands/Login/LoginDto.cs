namespace Product.Application.Features.Auth.Commands.Login
{
    public class LoginDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
