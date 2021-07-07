using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOSAPI.Interfaces;
using HOSAPI.Interfaces.Jobs;
using HOSAPI.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace HOSAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseJob, HOSDbJob>();

            return services;
        }
    }
}
