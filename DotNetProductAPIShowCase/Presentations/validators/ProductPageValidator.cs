using System;
using DotNetProductAPIShowCase.Services;
using FluentValidation;

namespace DotNetProductAPIShowCase.Presentations.validators;

public class ProductPageValidator : AbstractValidator<ProductPageDTO>
{
    public ProductPageValidator()
    {
        RuleFor(productPageDTO => productPageDTO.Page)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0");

        RuleFor(productPageDTO => productPageDTO.PageSize)
            .NotNull()
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .WithMessage("Page size must be between 1 and 100");
    }
}
