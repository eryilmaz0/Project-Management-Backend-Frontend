using System;
using JiraProject.Business.Exceptions;
using JiraProject.Entities.Entities;
using JiraProject.Entities.Extensions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace JiraProject.API.CustomMiddlewares
{
    public class ExceptionHandlerMiddleware
    {
        private RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }



        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            if (ex.GetType() == typeof(ValidationException))
            {
                return context.Response.ValidationErrorResponse(ex as ValidationException);
            }

            return context.Response.InternalServerErrorResponse();
        }
    }
}