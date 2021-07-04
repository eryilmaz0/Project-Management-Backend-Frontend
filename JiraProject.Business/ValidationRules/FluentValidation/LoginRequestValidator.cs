using FluentValidation;
using JiraProject.Entities.DataTransferObjects.Request;

namespace JiraProject.Business.ValidationRules.FluentValidation
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email adresi boş olamaz.").EmailAddress().WithMessage("Email Doğru Formatta Değil.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Boş Olamaz.")
                    .MinimumLength(6).WithMessage("Şifre En Az 6 Karakter Olmalı.");

          

        }
    }
}