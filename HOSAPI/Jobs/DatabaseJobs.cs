using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using HOSAPI.Configurations;
using HOSAPI.Contexts;
using HOSAPI.Core;
using HOSAPI.Interfaces;
using HOSAPI.Interfaces.Jobs;
using HOSAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HOSAPI.Jobs
{
    public class DatabaseJobs : IDatabaseJob
    {
        private readonly AuthService _agentEarningSyncAuth = new AuthService();

        private readonly SocarDbContext _socarDbContext;
        private readonly ILogger<DatabaseJobs> _logger;
        private readonly IOptions<HangfireConfiguration> _hangfireConfiguration;
        private readonly IRecurringJobManager _recurringJobManager;


        public DatabaseJobs(SocarDbContext socarDbContext,
            ILogger<DatabaseJobs> logger,
            IOptions<HangfireConfiguration> hangfireConfiguration,
            IRecurringJobManager recurringJobManager)
        {
            _socarDbContext = socarDbContext;
            _logger = logger;
            _hangfireConfiguration = hangfireConfiguration;
            _recurringJobManager = recurringJobManager;
        }

        public void ExecuteJob()
        {
            var auth = false;
            try
            {
                var userName = _hangfireConfiguration.Value.UserName;
                var password = _hangfireConfiguration.Value.Password;

                auth = _agentEarningSyncAuth.Authenticate(userName, password);

                if (auth)
                {
                    _logger.LogInformation($"User Authenticated");

                    _recurringJobManager.AddOrUpdate("DatabaseJobId", () => UpdateDatabase(), "*/1 * * * *");

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
            finally
            {
                if (auth)
                {
                    _agentEarningSyncAuth.Unauthenticate();

                    _logger.LogInformation($"User Unauthenticated");
                }
            }
        }

        public void UpdateDatabase()
        {
            Console.WriteLine("DatabaseJobId runs");

            _logger.LogInformation($"Job '{nameof(SocarDbContext)}' started");


            _logger.LogError("UpdateDatabase");

            var entity = new Comment()
            {
                CommentContent = "first Comment",
                ProductId = 2,
                ProductId1 = 3,
                UserId = 1
            };

            var a = _socarDbContext.Add(entity).Entity;

            _logger.LogInformation("Added Db  " + a.CommentId);

            _socarDbContext.SaveChanges();
        }
    }
}
