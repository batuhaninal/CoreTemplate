using Application.Models.DTOs.Products;
using Application.Models.Messages;
using Application.Utilities.Helpers;
using FluentValidation;

namespace Application.Utilities.FluentValidations.Products
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                    .WithMessage(CommonMessage.Validation.NotNull("Product Id"))
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull("Product Id"))
                .MaximumLength(50)
                    .WithMessage(CommonMessage.Validation.MaxLength("Product Id", 50));

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

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(1)
                    .WithMessage(CommonMessage.Validation.GreaterThanOrEqual("Price", 1));


            RuleFor(x => x.CategoryId)
                .NotEmpty()
                    .WithMessage(CommonMessage.Validation.NotNull("Category Id"))
                .NotNull()
                    .WithMessage(CommonMessage.Validation.NotNull("Category Id"))
                .MaximumLength(50)
                    .WithMessage(CommonMessage.Validation.MaxLength("Category Id", 50));


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
