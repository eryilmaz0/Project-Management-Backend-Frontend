using FluentValidation;
using JiraProject.Entities.DataTransferObjects.Request;

namespace JiraProject.Business.ValidationRules.FluentValidation
{
    public class EditTaskRequestValidator : AbstractValidator<EditTaskRequest>
    {
        public EditTaskRequestValidator()
        {
            RuleFor(x => x.TaskDescription).NotEmpty().WithMessage("Görev Açıklaması Boş Olamaz.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("Geçersiz Durum Değeri.");
            RuleFor(x => x.Priority).IsInEnum().WithMessage("Geçersiz Öncelik Değeri.");
            RuleFor(x => x.AssignedUserId).GreaterThan(0).WithMessage("Atanan Kullanıcı Boş Olamaz.");
        }
    }
}