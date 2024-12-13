using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Product.Infastructure.Configurations
{
    public static class InfastructureServiceRegistration
    {
        public static IServiceCollection AddInfastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            //services.AddScoped<IEmailSender, EmailSender>();
            return services;
        }
    }
}
