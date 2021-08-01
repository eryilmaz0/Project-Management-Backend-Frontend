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
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        [HttpGet]
        [Authorize]
        public IActionResult GetDepartments()
        {
            var result = _departmentService.GetDepartments();

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest("Departmanlar Listelenirken Bir Hata Oluştu.");
        }



        [HttpGet]
        [Route("{departmentId:int}")]
        public IActionResult GetDepartment(int departmentId)
        {
            var result = _departmentService.GetDepartments();
            var department = result.Data.FirstOrDefault(x => x.Id == departmentId);

            if (department == null)
            {
                return BadRequest("Departman Bulunamadı.");
            }

            return Ok(new {isSuccess = true, data = department});
        }
    }
}
