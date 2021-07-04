using System.Collections.Generic;

namespace JiraProject.Business.ValidationRules
{
    public class ValidationErrorDetail
    {
        public string PropertyName { get; set; }
        public ICollection<string> ValidationErrorMessages { get; set; }


        public ValidationErrorDetail(string propertyName, ICollection<string> validationErrorMessages)
        {
            this.PropertyName = propertyName;
            this.ValidationErrorMessages = validationErrorMessages;
        }


        public ValidationErrorDetail()
        {
            this.ValidationErrorMessages = new List<string>();
        }
    }
}