using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using HOSAPI.Contexts;
using HOSAPI.Extensions;
using HOSAPI.Interfaces;
using HOSAPI.Interfaces.Jobs;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.RollingFileAlternative;

namespace HOSAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterConfiguration(Configuration); 
            
            services.RegisterServices();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            services.AddDbContext<SocarDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SocarDbConnection")));

            services.AddHangfire(configuration =>
                configuration.UseSqlServerStorage(Configuration.GetConnectionString("HangfireDbConnection"),
                        new SqlServerStorageOptions()
                        {
                            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                            QueuePollInterval = TimeSpan.Zero,
                            UseRecommendedIsolationLevel = true,
                            DisableGlobalLocks = true
                        })
                    .UseSerilogLogProvider()
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                );

            services.AddHangfireServer();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            IServiceProvider serviceProvider, IDatabaseJob databaseJob)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Log.Logger = new LoggerConfiguration()
                .WriteTo.RollingFile(System.IO.Path.Combine("../logs/{Date}-logs.txt"))
                .CreateLogger();

            app.UseHttpsRedirection();

            app.UseHangfireServer();
            
            app.UseHangfireDashboard();

            databaseJob.ExecuteJob();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
