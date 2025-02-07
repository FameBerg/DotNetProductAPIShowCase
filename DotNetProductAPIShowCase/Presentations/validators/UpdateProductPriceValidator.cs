using System;
using DotNetProductAPIShowCase.Applications.DTOS;
using FluentValidation;

namespace DotNetProductAPIShowCase.Presentations.validators;

public class UpdateProductPriceValidator : AbstractValidator<UpdateProductPriceDTO>
{
    public UpdateProductPriceValidator()
    {
        RuleFor(productDTO => productDTO.Price)
            .NotNull()
            .GreaterThan(0).WithMessage("Price must be greater than zero.")
            .LessThanOrEqualTo(99999999.99m).WithMessage("Price must be less than or equal to 99,999,999.99.")
            .PrecisionScale(10, 2, false).WithMessage("Price must have up to 2 decimal places and be within the valid range.");
    }
}
