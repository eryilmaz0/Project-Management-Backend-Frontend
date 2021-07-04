using System;
using System.Collections.Generic;
using JiraProject.Business.ValidationRules;

namespace JiraProject.Business.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationError ValidationError { get; set; }


        public ValidationException(ValidationError validationErrors)
        {
            this.ValidationError = validationErrors;
        }
    }
}