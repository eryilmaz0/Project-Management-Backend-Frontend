using FluentValidation;
using JiraProject.Entities.DataTransferObjects.Request;

namespace JiraProject.Business.ValidationRules.FluentValidation
{
    public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskRequestValidator()
        {
            RuleFor(x => x.TaskDescription).NotNull().WithMessage("Görev Açıklaması Boş Olamaz.");
            RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("Proje Numarası Boş Olamaz.");
            RuleFor(x => x.Priority).IsInEnum().WithMessage("Geçersiz Öncelik Değeri.");
            RuleFor(x => x.Type).IsInEnum().WithMessage("Geçersiz Görev Türü Değeri.");
            RuleFor(x => x.AssignedUserId).GreaterThan(0).WithMessage("Kullanıcı Numarası Boş Olamaz.");
        }
    }
}