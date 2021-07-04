using System.Collections;
using System.Collections.Generic;

namespace JiraProject.Business.ValidationRules
{
    public class ValidationError : ResponseErrorDetails
    {
        public ICollection<ValidationErrorDetail> ValidationErrorDetails { get; set; }


        public ValidationError(string message, int statusCode,ICollection<ValidationErrorDetail> validationErrorDetails):base(message,statusCode)
        {
            this.ValidationErrorDetails = validationErrorDetails;
        }
    }
}