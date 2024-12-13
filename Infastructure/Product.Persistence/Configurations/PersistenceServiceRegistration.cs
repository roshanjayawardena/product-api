using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Product.Application.Contracts.Infastructure.Auth;
using Product.Application.Contracts.Persistence;
using Product.Application.Models.Auth;
using Product.Persistence.Context;
using Product.Persistence.Models;
using Product.Persistence.Repositories;
using Product.Persistence.Services.Identity;
using System.Text;

namespace Product.Persistence.Configurations
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ProductConnectionString")));
            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ProductDbContext>().AddDefaultTokenProviders();
            services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));
            services.AddScoped<IAuthService, AuthService>();
          

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(u => u.TokenValidationParameters = new TokenValidationParameters
            {

                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = configuration.GetSection("JWTSettings:Issuer").Value,
                ValidAudience = configuration.GetSection("JWTSettings:Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWTSettings:Key").Value)),
                ClockSkew = TimeSpan.Zero
            });

            return services;
        }
    }
}
