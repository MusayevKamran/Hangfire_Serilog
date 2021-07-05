using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Logging;
using HOSAPI.Contexts;
using HOSAPI.Models;
using Microsoft.Extensions.Logging;

namespace HOSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : ControllerBase
    {
        private readonly SocarDbContext _socarDbContext;
        private readonly ILogger<HangfireController> _logger;

        public HangfireController(SocarDbContext socarDbContext, ILogger<HangfireController> logger)
        {
            _socarDbContext = socarDbContext;
            _logger = logger;
        }

        #region Posts

        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateDb()
        {

            RecurringJob.AddOrUpdate(() => UpdateDatabase(), "*/1 * * * *");

            return Ok("Job updated");
        }

        public void UpdateDatabase()
        {
            _logger.LogError("Added Db  1");
            _logger.LogWarning("Added Db  2");
            _logger.LogCritical("Added Db  3");

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

        #endregion
    }
}

