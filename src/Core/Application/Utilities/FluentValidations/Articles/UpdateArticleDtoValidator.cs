using Application.Models.DTOs.Articles;
using Application.Models.Messages;
using Application.Utilities.Helpers;
using FluentValidation;

namespace Application.Utilities.FluentValidations.Products
{
    public class UpdateArticleDtoValidator : AbstractValidator<UpdateArticleDto>
    {
        public UpdateArticleDtoValidator()
        {
            RuleFor(x => x.ArticleId)
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull());

            RuleFor(x => x.Title)
                .NotEmpty()
                    .WithMessage(CommonMessage.Validation.NotNull("Title"))
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull("Title"))
                .MinimumLength(2)
                    .WithMessage(CommonMessage.Validation.MinLength("Title", 2))
                .MaximumLength(100)
                    .WithMessage(CommonMessage.Validation.MaxLength("Title", 100))
                .Must(RegexHelper.CheckWhiteSpaceDuplicate)
                    .WithMessage(CommonMessage.RegexErr.DuplicateWhiteSpace("Title"));

            RuleFor(x => x.Content)
                .NotEmpty()
                    .WithMessage(CommonMessage.Validation.NotNull("Content"))
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull("Content"))
                .MinimumLength(2)
                    .WithMessage(CommonMessage.Validation.MinLength("Content", 2))
                .MaximumLength(5000)
                    .WithMessage(CommonMessage.Validation.MaxLength("Content", 5000));


            RuleFor(x => x.CategoryId)
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull("Category Id"));


            // Ornek nullable
            //RuleFor(x => x.Title)
            //    .MinimumLength(2)
            //        .WithMessage(CommonMessage.Validation.MinLength("Title", 2))
            //    .MaximumLength(100)
            //        .WithMessage(CommonMessage.Validation.MaxLength("Title", 100))
            //    .Must(RegexHelper.CheckWhiteSpaceDuplicate)
            //        .WithMessage(CommonMessage.RegexErr.DuplicateWhiteSpace("Title"))
            //    .When(x => string.IsNullOrEmpty(x.Title))
        }
    }
}
