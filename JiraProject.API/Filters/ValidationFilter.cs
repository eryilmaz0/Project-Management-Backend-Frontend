using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using JiraProject.Business.Exceptions;
using JiraProject.Business.ValidationRules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ValidationException = JiraProject.Business.Exceptions.ValidationException;

namespace JiraProject.API.Filters
{



    public class ValidationFilter : TypeFilterAttribute
    {
        public ValidationFilter():base(typeof(HandleValidationsFilter))
        {
            
        }
    }





    public class HandleValidationsFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {
                //Modelstate'deki validation hataları ile bir validationerror oluştur.
                ICollection<ValidationErrorDetail> validationErrorDetails = context.ModelState.Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new ValidationErrorDetail(x.Key, x.Value.Errors.Select(x=>x.ErrorMessage).ToList())).ToList();

                ValidationError validationError = new ValidationError("Validation Error",400,validationErrorDetails);

                //validationexception fırlat. exceptionmiddleware'de bunu yakala, ve 400 badrequest dön.
                throw new ValidationException(validationError);
               
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}