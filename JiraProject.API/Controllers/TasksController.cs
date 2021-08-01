using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiraProject.API.Filters;
using JiraProject.Business.Abstract;
using JiraProject.Entities.DataTransferObjects.Request;
using Microsoft.AspNetCore.Authorization;

namespace JiraProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        private readonly ITaskService _taskService;


        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }


        [HttpGet]
        [Route("Project/{projectId:int}")]
        [Authorize]
        public IActionResult GetTasksByProject(int projectId)
        {
            var result = _taskService.GetTasksByProject(projectId);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpGet]
        [Route("{taskId:int}")]
        [Authorize]
        public IActionResult GetTaskById(int taskId)
        {
            var result = _taskService.GetTaskById(taskId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }



        [HttpGet]
        [Route("{taskId:int}/Changes")]
        [Authorize]
        public IActionResult GetTaskChangesByTask(int taskId)
        {
            var result = _taskService.GetTaskChangesByTask(taskId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }




        [HttpPost]
        [Authorize(Roles = "Leader")]
        [ValidationFilter]
        public IActionResult CreateTask(CreateTaskRequest request)
        {
            var result = _taskService.CreateTask(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }




        [HttpPut]
        [Authorize]
        [ValidationFilter]
        public IActionResult EditTask(EditTaskRequest request)
        {
            var result = _taskService.EditTask(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }


            return BadRequest(result);
        }


        
    }
}
