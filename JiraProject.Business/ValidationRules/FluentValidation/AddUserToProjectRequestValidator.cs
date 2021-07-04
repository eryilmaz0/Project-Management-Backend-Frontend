using FluentValidation;
using JiraProject.Entities.DataTransferObjects.Request;

namespace JiraProject.Business.ValidationRules.FluentValidation
{
    public class AddUserToProjectRequestValidator : AbstractValidator<AddUserToProjectRequest>
    {
        public AddUserToProjectRequestValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("Kullanıcı Numarası Boş Olamaz.");
            RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("Proje Numarası Boş Olamaz.");
        }
    }
}