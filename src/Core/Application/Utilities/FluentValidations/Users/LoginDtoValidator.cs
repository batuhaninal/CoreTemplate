using Application.Models.DTOs.Auths;
using Application.Models.Messages;
using Application.Utilities.Helpers;
using FluentValidation;

namespace Application.Utilities.FluentValidations.Users
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage(CommonMessage.Validation.NotNull("Email"))
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull("Email"))
                .MaximumLength(100)
                    .WithMessage(CommonMessage.Validation.MaxLength("Email", 100))
                .EmailAddress()
                    .WithMessage(CommonMessage.Validation.Email)
                .Must(RegexHelper.CheckWhiteSpaceExist)
                    .WithMessage(CommonMessage.RegexErr.WhiteSpace("Email"));

            RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage(CommonMessage.Validation.NotNull("Password"))
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull("Password"))
                .MaximumLength(100)
                    .WithMessage(CommonMessage.Validation.MaxLength("Password", 100))
                .Must(RegexHelper.CheckWhiteSpaceExist)
                    .WithMessage(CommonMessage.RegexErr.WhiteSpace("Password"));

        }
    }
}
