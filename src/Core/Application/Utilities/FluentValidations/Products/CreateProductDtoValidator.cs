using Application.Models.DTOs.Products;
using Application.Models.Messages;
using Application.Utilities.Helpers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilities.FluentValidations.Products
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
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
        }
    }
}
