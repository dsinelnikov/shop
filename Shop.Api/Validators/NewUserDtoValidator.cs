using FluentValidation;
using Shop.Services.Dto.Products.Authentication;

namespace Shop.Api.Validators
{
    public class NewUserDtoValidator : AbstractValidator<NewUserDto>
    {
        public NewUserDtoValidator()
        {
            RuleFor(u => u.Email).NotEmpty()
                .EmailAddress();
            RuleFor(u => u.Password).NotEmpty();
        }
    }
}
