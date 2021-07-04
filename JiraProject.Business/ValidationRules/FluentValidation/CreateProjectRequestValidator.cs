using FluentValidation;
using JiraProject.Entities.DataTransferObjects.Request;

namespace JiraProject.Business.ValidationRules.FluentValidation
{
    public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
    {
        public CreateProjectRequestValidator()
        {
            RuleFor(x => x.ProjectName).NotEmpty().WithMessage("Proje Adı Boş Olamaz.");
            RuleFor(x => x.ProjectDescription).NotEmpty().WithMessage("Proje Açıklaması Boş Olamaz.");
            RuleFor(x => x.DepartmentId).GreaterThan(0).WithMessage("Departman Numarası Boş Olamaz.");
            ;
        }

    }
}