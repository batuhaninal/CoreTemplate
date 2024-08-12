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
                    .WithMessage(CommonMessage.Validation.NotNull())
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull())
                .MaximumLength(100)
                    .WithMessage(CommonMessage.Validation.MaxLength())
                .EmailAddress()
                    .WithMessage(CommonMessage.Validation.Email)
                .Must(RegexHelper.CheckWhiteSpaceExist)
                    .WithMessage(CommonMessage.RegexErr.WhiteSpace());

            RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage(CommonMessage.Validation.NotNull())
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull())
                .MinimumLength(5)
                    .WithMessage(CommonMessage.Validation.MinLength())
                .MaximumLength(100)
                    .WithMessage(CommonMessage.Validation.MaxLength())
                .Must(RegexHelper.CheckWhiteSpaceExist)
                    .WithMessage(CommonMessage.RegexErr.WhiteSpace());

            RuleFor(x => x.RepeatPassword)
                .NotEmpty()
                    .WithMessage(CommonMessage.Validation.NotNull())
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull())
                .MaximumLength(100)
                    .WithMessage(CommonMessage.Validation.MaxLength())
                .Equal(x => x.Password)
                    .WithMessage(CommonMessage.Validation.PasswordsNotMatches);

            RuleFor(u => u.FirstName)
                .NotEmpty()
                    .WithMessage(CommonMessage.Validation.NotNull())
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull())
                .MaximumLength(50)
                    .WithMessage(CommonMessage.Validation.MaxLength())
                .MinimumLength(2)
                    .WithMessage(CommonMessage.Validation.MinLength())
                .Must(RegexHelper.CheckWhiteSpaceDuplicate)
                    .WithMessage(CommonMessage.RegexErr.DuplicateWhiteSpace());

            RuleFor(u => u.LastName)
                .NotEmpty()
                    .WithMessage(CommonMessage.Validation.NotNull())
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull())
                .MaximumLength(50)
                    .WithMessage(CommonMessage.Validation.MaxLength())
                .MinimumLength(2)
                    .WithMessage(CommonMessage.Validation.MinLength())
                .Must(RegexHelper.CheckWhiteSpaceDuplicate)
                    .WithMessage(CommonMessage.RegexErr.DuplicateWhiteSpace());

            RuleFor(x => x.Nick)
                .NotEmpty()
                    .WithMessage(CommonMessage.Validation.NotNull())
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull())
                .MaximumLength(50)
                    .WithMessage(CommonMessage.Validation.MaxLength())
                .Must(RegexHelper.CheckWhiteSpaceExist)
                    .WithMessage(CommonMessage.RegexErr.WhiteSpace());
        }
    }
}
