using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimesheetApp.API.Dto;
using TimesheetApp.API.Services;

namespace TimesheetApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : Controller
    {
        private readonly IServices _service;

        public LogController(IServices service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<LogDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> GetAllLogs()
        {
            var logList = await _service.GetAllLogs();
            return StatusCode(200, logList);
        }

        [HttpPost]
        [ProducesResponseType(typeof(LogDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> CreateLog(LogToCreateDto logToCreate)
        {
            var log = await _service.CreateLog(logToCreate);

            if (log == null)
            {
                return StatusCode(400);
            }
            return StatusCode(200, log);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LogDto), 200)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> GetLogById(int id)
        {
            var log = await _service.GetLogById(id);

            if (log == null)
            {
                return StatusCode(401);
            }
            return StatusCode(200, log);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(LogDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> UpdateLog(int id, LogToUpdateDto logToUpdate)
        {
            var log = await _service.UpdateLog(id, logToUpdate);

            if (log == null)
            {
                return StatusCode(400);
            }
            return StatusCode(200, log);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> DeleteLog(int id)
        {
            bool isDeleted = await _service.DeleteLog(id);

            if (isDeleted)
            {
                return StatusCode(200);
            }
            return StatusCode(400);
        }


    }
}