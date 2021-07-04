using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiraProject.Business.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace JiraProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskChangesController : ControllerBase
    {

        private readonly ITaskChangeService _taskChangeService;


        public TaskChangesController(ITaskChangeService taskChangeService)
        {
            _taskChangeService = taskChangeService;
        }



        [HttpGet]
        [Route("ByTask/{taskId:int}")]
        [Authorize]
        public IActionResult GetTaskChangesByTask(int taskId)
        {
            var result = _taskChangeService.GetTaskChangesByTask(taskId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
