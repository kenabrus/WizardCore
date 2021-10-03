using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Api.Models;
using Infrastructure.Mappers;

namespace Api.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IWizardService, WizardService>();           
            services.AddSingleton(configuration.GetSettings<EmailConfigSettings>());
            services.AddSingleton(AutoMapperConfig.Initialize());
            return services;
        }   
    }
}