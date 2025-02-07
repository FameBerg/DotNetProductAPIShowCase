using System;
using DotNetProductAPIShowCase.Applications.DTOS;
using FluentValidation;

namespace DotNetProductAPIShowCase.Presentations.validators;

public class ProductValidator : AbstractValidator<ProductDTO>
{
    public ProductValidator()
    {
        RuleFor(productDTO => productDTO.Name)
            .NotNull()
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");
        RuleFor(productDTO => productDTO.Price)
            .NotNull()
            .GreaterThan(0).WithMessage("Price must be greater than zero.")
            .LessThanOrEqualTo(99999999.99m).WithMessage("Price must be less than or equal to 99,999,999.99.")
            .PrecisionScale(10, 2, false).WithMessage("Price must have up to 2 decimal places and be within the valid range.");
        RuleFor(productDTO => productDTO.Description)
            .MaximumLength(500).WithMessage("description cannot exceed 500 characters.");
    }
}
