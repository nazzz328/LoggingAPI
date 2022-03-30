using LoggingAPI.Context;
using LoggingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoggingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {

        private readonly LoggingContext db;
        public LogsController(LoggingContext context)
        {
            db = context;
        }

        #region Frontend

        [HttpGet]
        [Route("Frontend")]
        public async Task<IActionResult> FrontendGet()
        {
            List<FrontendLog> frontendLogs;
            try
            {
                frontendLogs = await db.FrontendLogs.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(frontendLogs);
        }

        [HttpPost]
        [Route("Frontend")]
        public async Task<IActionResult> FrontendPost(FrontendLog frontendLog)
        {
            try
            {
                await db.FrontendLogs.AddAsync(frontendLog);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        #endregion

        #region Backend

        [HttpGet]
        [Route("Backend")]
        public async Task<IActionResult> BackendGet()
        {
            List<BackendLog> backendLogs;
            try
            {
                backendLogs = await db.BackendLogs.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(backendLogs);
        }

        [HttpPost]
        [Route("Backend")]
        public async Task<IActionResult> BackendPost(BackendLog backendLog)
        {
            try
            {
                await db.BackendLogs.AddAsync(backendLog);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        #endregion

        #region Action

        [HttpGet]
        [Route("Action")]
        public async Task<IActionResult> ActionGet()
        {
            List<ActionLog> actionLogs;
            try
            {
                actionLogs = await db.ActionLogs.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(actionLogs);
        }

        [HttpPost]
        [Route("Action")]
        public async Task<IActionResult> ActionPost(ActionLog actionLog)
        {
            try
            {
                await db.ActionLogs.AddAsync(actionLog);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        #endregion
    }
}
