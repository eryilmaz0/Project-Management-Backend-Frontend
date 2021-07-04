using FluentValidation;
using JiraProject.Entities.DataTransferObjects.Request;

namespace JiraProject.Business.ValidationRules.FluentValidation
{
    public class EditProjectRequestValidator : AbstractValidator<EditProjectRequest>
    {
        public EditProjectRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Proje Numarası Boş Olamaz.");
            RuleFor(x => x.ProjectLeaderId).GreaterThan(0).WithMessage("Proje Lideri Numarası Boş Olamaz.");
            RuleFor(x => x.ProjectName).NotEmpty().WithMessage("Proje Adı Boş Olamaz.");
            RuleFor(x => x.ProjectDescription).NotEmpty().WithMessage("Proje Açıklaması Boş Olamaz.");
            RuleFor(x => x.DepartmentId).GreaterThan(0).WithMessage("Departman Numarası Boş Olamaz.");
            RuleFor(x => x.Created).NotEmpty().WithMessage("Oluşturulma Tarihi Boş Olamaz.");
        }
    }
}