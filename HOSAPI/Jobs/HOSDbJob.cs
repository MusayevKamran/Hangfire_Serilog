using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
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
    public class HOSDbJob : IDatabaseJob
    {
        private readonly SocarDbContext _socarDbContext;
        private readonly ILogger<HOSDbJob> _logger;
        private readonly IOptions<HangfireConfiguration> _hangfireConfiguration;
        private readonly IRecurringJobManager _recurringJobManager;


        public HOSDbJob(SocarDbContext socarDbContext,
            ILogger<HOSDbJob> logger,
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
            _recurringJobManager.AddOrUpdate("DatabaseJobId", () => DatabaseJob(), "*/1 * * * *");
        }

        public void DatabaseJob()
        {
            AuthService agentEarningSyncAuth = new AuthService(_socarDbContext);

            try
            {
                var userName = _hangfireConfiguration.Value.UserName;
                var password = _hangfireConfiguration.Value.Password;

                var auth = agentEarningSyncAuth.Authenticate(userName, password);

                if (!auth)
                    _logger.LogError($"User Unauthenticated");

                _logger.LogInformation($"User Authenticated");

                if (auth)
                    UpdateDatabase();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new AuthenticationException("Auth Error");
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
