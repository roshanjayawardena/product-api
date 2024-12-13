
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Product.Api.Middlewares;
using Product.Api.Services;
using Product.Application.Configurations;
using Product.Application.Contracts.Infastructure.Auth;
using Product.Infastructure.Configurations;
using Product.Persistence.Configurations;
using Product.Persistence.Context;

namespace Product.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clean architecture", Version = "v1", });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
            builder.Services.AddApplicationService()
                            .AddPersistenceService(builder.Configuration)
            .AddInfastructureService(builder.Configuration);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            builder.Services.AddCors(p => p.AddDefaultPolicy(build =>
            {
                build.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            }));

            var app = builder.Build();

            // Ensure the database is created
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ProductDbContext>();

                // Apply pending migrations
                await dbContext.Database.MigrateAsync();

                // Seed roles and admin user
                await IdentitySeeder.SeedRolesAndAdminAsync(services);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
          
            app.UseExceptionHandler();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();           
        }
    }
}
