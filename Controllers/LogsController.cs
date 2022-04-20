using System.Text.Json;
using System.Text.Json.Serialization;
using LoggingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace LoggingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class logsController : ControllerBase
    {
        IMongoCollection<FrontendLog> _frontendLogsCollection;
        IMongoCollection<BackendLog> _backendLogsCollection;
        IMongoCollection<ActionLog> _actionLogsCollection;

        public logsController(IMongoDatabase database)
        {
            _frontendLogsCollection = database.GetCollection<FrontendLog>("frontendlogs");
            _backendLogsCollection = database.GetCollection<BackendLog>("backendlogs");
            _actionLogsCollection = database.GetCollection<ActionLog>("actionlogs");
        }

        #region Frontend

        [HttpGet, Authorize]
        [Route("frontend")]
        public async Task<IActionResult> FrontendGet()
        {
            List<FrontendLog> frontendLogs;
            try
            {
                frontendLogs = await _frontendLogsCollection.Find(_ => true).ToListAsync();
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
                await _frontendLogsCollection.InsertOneAsync(frontendLog);
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
            List<BackendLog> backendLogs;
            try
            {
                backendLogs = await _backendLogsCollection.Find(_ => true).ToListAsync();
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
                await _backendLogsCollection.InsertOneAsync(backendLog);
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
            List<ActionLog> actionLogs;
            try
            {
                actionLogs = await _actionLogsCollection.Find(_ => true).ToListAsync();
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
                actionLog.Prev_Value = System.Text.Json.JsonSerializer.Serialize(actionLog.Prev_Value);
                actionLog.New_Value = System.Text.Json.JsonSerializer.Serialize(actionLog.New_Value);
                await _actionLogsCollection.InsertOneAsync(actionLog);
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
