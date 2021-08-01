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
    public class ProjectsController : ControllerBase
    {

        private readonly IProjectService _projectService;


        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }



        [HttpGet]
        [Route("{departmentId:int}")]
        [Authorize]
        public IActionResult GetProjectsByDepartment(int departmentId)
        {
            var result = _projectService.GetProjectsByDepartment(departmentId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }


        [HttpGet]
        [Authorize]
        public IActionResult GetAllProjects()
        {
            var result = _projectService.GetProjects();

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }


        [HttpGet]
        [Route("Filter/{filter}")]
        [Authorize]
        public IActionResult GetProjectsByFilter(string filter)
        {
            var result = _projectService.GetProjectsByFilter(filter);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }


        [HttpGet]
        [Route("Detail/{projectId:int}")]
        [Authorize]
        public IActionResult GetProjectWithDetail(int projectId)
        {
            var result = _projectService.GetProjectDetail(projectId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }



        [HttpPost]
        [Authorize(Roles = "Leader")]
        [ValidationFilter]
        public IActionResult CreateProject(CreateProjectRequest request)
        {
            var result = _projectService.CreateProject(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPut]
        [Authorize(Roles = "Leader")]
        [ValidationFilter]
        public IActionResult EditProject(EditProjectRequest request)
        {
            var result = _projectService.EditProject(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }




        [HttpGet]
        [Route("{projectId:int}/Activate")]
        [Authorize(Roles = "Leader")]
        public IActionResult ActivateProject(int projectId)
        {
            var result = _projectService.ActivateProject(projectId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }




        [HttpGet]
        [Route("{projectId:int}/Deactivate")]
        [Authorize(Roles = "Leader")]
        public IActionResult DeactivateProject(int projectId)
        {
            var result = _projectService.DeactivateProject(projectId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }




        [HttpPost]
        [Route("AddUser")]
        [Authorize(Roles = "Leader")]
        [ValidationFilter]
        public IActionResult AddUserToProject([FromQuery] AddUserToProjectRequest request)
        {
            var result = _projectService.AddUserToProject(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }




        [HttpDelete]
        [Route("RemoveUser")]
        [Authorize(Roles = "Leader")]
        [ValidationFilter]
        public IActionResult RemoveUserFromProject([FromQuery] RemoveUserFromProjectRequest request)
        {
            var result = _projectService.RemoveUserFromProject(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }




        [HttpGet]
        [Route("{projectId:int}/Members")]
        [Authorize]
        public IActionResult GetProjectMembers(int projectId)
        {
            var result = _projectService.GetProjectMembers(projectId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }




        [HttpGet]
        [Route("{projectId:int}/OutOfMembers")]
        [Authorize]
        public IActionResult GetUsersOutOfProject(int projectId)
        {
            var result = _projectService.GetUsersOutOfTheProject(projectId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }



    }
}
