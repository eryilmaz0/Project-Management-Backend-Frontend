using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using JiraProject.API.Filters;
using JiraProject.Business.Abstract;
using JiraProject.Business.ValidationRules.FluentValidation;
using JiraProject.Entities.DataTransferObjects.Request;
using Microsoft.AspNetCore.Authorization;

namespace JiraProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;


        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }



        [HttpPost]
        [Route("Login")]
        [ValidationFilter]
        public IActionResult Login(LoginRequest loginRequest)
        {
            //IValidator validator = new LoginRequestValidator();
            //var context = new ValidationContext<LoginRequest>(loginRequest);
            //var result2 = validator.Validate(context);

            var result = _authService.Login(loginRequest);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            
            return BadRequest(result.Message);
        }





        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterRequest registerRequest)
        {
            var result = _authService.Register(registerRequest);

            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }


    }
}
