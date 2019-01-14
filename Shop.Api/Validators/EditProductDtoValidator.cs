using FluentValidation;
using Shop.Services.Dto.Products;

namespace Shop.Api.Validators
{
    public class EditProductDtoValidator : AbstractValidator<EditProductDto>
    {
        public EditProductDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Price).GreaterThanOrEqualTo(0);
            RuleFor(p => p.BrandId).GreaterThan(0);
            RuleFor(p => p.CategoryId).GreaterThan(0);
            RuleFor(p => p.ManufactureCountryId).GreaterThan(0);
        }
    }
}
