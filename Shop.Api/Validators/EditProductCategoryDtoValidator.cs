using FluentValidation;
using Shop.Services.Dto.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Api.Validators
{
    public class EditProductCategoryDtoValidator : AbstractValidator<EditProductCategoryDto>
    {
        public EditProductCategoryDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
