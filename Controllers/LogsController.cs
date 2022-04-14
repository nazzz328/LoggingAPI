using LoggingAPI.Dapper_Repositories;
using LoggingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoggingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class logsController : ControllerBase
    {
        private readonly ActionLogRepository actionLogRepository;
        private readonly BackendLogRepository backendLogRepository;
        private readonly FrontendLogRepository frontendLogRepository;

        public logsController(IConfiguration configuration)
        {
            actionLogRepository = new ActionLogRepository(configuration);
            backendLogRepository = new BackendLogRepository(configuration);
            frontendLogRepository = new FrontendLogRepository(configuration);
        }

        #region Frontend

        [HttpGet, Authorize]
        [Route("frontend")]
        public async Task<IActionResult> FrontendGet()
        {
            IEnumerable<FrontendLog> frontendLogs;
            try
            {
                frontendLogs = frontendLogRepository.FindAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(frontendLogs);
        }

        [HttpPost, Authorize]
        [Route("frontend")]
        public async Task<IActionResult> FrontendPost(FrontendLog frontendLog)
        {
            try
            {
                frontendLogRepository.Add(frontendLog);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        #endregion

        #region Backend

        [HttpGet, Authorize]
        [Route("backend")]
        public async Task<IActionResult> BackendGet()
        {
            IEnumerable<BackendLog> backendLogs;
            try
            {
                backendLogs = backendLogRepository.FindAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(backendLogs);
        }

        [HttpPost, Authorize]
        [Route("backend")]
        public async Task<IActionResult> BackendPost(BackendLog backendLog)
        {
            try
            {
                backendLogRepository.Add(backendLog);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        #endregion

        #region Action

        [HttpGet, Authorize]
        [Route("action")]
        public async Task<IActionResult> ActionGet()
        {
            IEnumerable<ActionLog> actionLogs;
            try
            {
                 actionLogs = actionLogRepository.FindAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(actionLogs);
        }

        [HttpPost, Authorize]
        [Route("action")]
        public async Task<IActionResult> ActionPost(ActionLog actionLog)
        {
            try
            {
                actionLogRepository.Add(actionLog);
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
