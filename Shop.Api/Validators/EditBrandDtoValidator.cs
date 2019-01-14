using FluentValidation;
using Shop.Services.Dto.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Api.Validators
{
    public class EditBrandDtoValidator : AbstractValidator<EditBrandDto>
    {
        public EditBrandDtoValidator()
        {
            RuleFor(b => b.Name).NotEmpty();
            RuleFor(b => b.RegistrationCountryId).NotEmpty();
        }
    }
}
