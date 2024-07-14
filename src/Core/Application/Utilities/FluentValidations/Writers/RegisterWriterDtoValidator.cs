using Application.Models.DTOs.Writers;
using Application.Models.Messages;
using Application.Utilities.Helpers;
using FluentValidation;

namespace Application.Utilities.FluentValidations.Writers
{
    public class RegisterWriterDtoValidator : AbstractValidator<RegisterWriterDto>
    {
        public RegisterWriterDtoValidator()
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
                .MinimumLength(5)
                    .WithMessage(CommonMessage.Validation.MinLength("Password", 5))
                .MaximumLength(100)
                    .WithMessage(CommonMessage.Validation.MaxLength("Password", 100))
                .Must(RegexHelper.CheckWhiteSpaceExist)
                    .WithMessage(CommonMessage.RegexErr.WhiteSpace("Password"));

            RuleFor(x => x.RepeatPassword)
                .NotEmpty()
                    .WithMessage(CommonMessage.Validation.NotNull("Repeat Password"))
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull("Repeat Password"))
                .MaximumLength(100)
                    .WithMessage(CommonMessage.Validation.MaxLength("Repeat Password", 100))
                .Equal(x => x.Password)
                    .WithMessage(CommonMessage.Validation.PasswordsNotMatches);

            RuleFor(u => u.FirstName)
                .NotEmpty()
                    .WithMessage(CommonMessage.Validation.NotNull("First Name"))
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull("First Name"))
                .MaximumLength(50)
                    .WithMessage(CommonMessage.Validation.MaxLength("First Name", 50))
                .MinimumLength(2)
                    .WithMessage(CommonMessage.Validation.MinLength("First Name", 2))
                .Must(RegexHelper.CheckWhiteSpaceDuplicate)
                    .WithMessage(CommonMessage.RegexErr.DuplicateWhiteSpace("First Name"));

            RuleFor(u => u.LastName)
                .NotEmpty()
                    .WithMessage(CommonMessage.Validation.NotNull("Last Name"))
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull("Last Name"))
                .MaximumLength(50)
                    .WithMessage(CommonMessage.Validation.MaxLength("Last Name", 50))
                .MinimumLength(2)
                    .WithMessage(CommonMessage.Validation.MinLength("Last Name", 2))
                .Must(RegexHelper.CheckWhiteSpaceDuplicate)
                    .WithMessage(CommonMessage.RegexErr.DuplicateWhiteSpace("Last Name"));

            RuleFor(x => x.Nick)
                .NotEmpty()
                    .WithMessage(CommonMessage.Validation.NotNull("Nick"))
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull("Nick"))
                .MaximumLength(50)
                    .WithMessage(CommonMessage.Validation.MaxLength("Nick", 50))
                .Must(RegexHelper.CheckWhiteSpaceExist)
                    .WithMessage(CommonMessage.RegexErr.WhiteSpace("Nick"));
        }
    }
}
