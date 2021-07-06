using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOSAPI.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HOSAPI.Extensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<HangfireConfiguration>(configuration.GetSection("Hangfire"));

            return services;
        }
    }
}
