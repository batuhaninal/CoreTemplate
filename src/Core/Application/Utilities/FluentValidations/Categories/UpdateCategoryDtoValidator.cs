using Application.Models.DTOs.Categories;
using Application.Models.Messages;
using Application.Utilities.Helpers;
using FluentValidation;

namespace Application.Utilities.FluentValidations.Categories
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull("Category Id"))
                .MaximumLength(50)
                    .WithMessage(CommonMessage.Validation.MaxLength("Category Id", 50));

            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull("Title"))
                .MaximumLength(75)
                    .WithMessage(CommonMessage.Validation.MaxLength("Title", 75))
                .Must(RegexHelper.CheckWhiteSpaceDuplicate)
                    .WithMessage(CommonMessage.RegexErr.DuplicateWhiteSpace("Title"));
        }
    }
}
