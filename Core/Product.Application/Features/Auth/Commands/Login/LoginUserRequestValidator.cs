using FluentValidation;
using Product.Application.Helpers;

namespace Product.Application.Features.Auth.Commands.Login
{
    public class LoginUserRequestValidator :AbstractValidator<LoginUserRequest>
    {
        public LoginUserRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty.")
                    .NotNull().WithMessage("Email is required.")
                    .EmailAddress().WithMessage("Please enter the valid email address.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty.")
                   .NotNull().WithMessage("Password is required.")
                   .Must(ValidationHelper.IsValidPassword).WithMessage("Please enter valid password.");
        }
    }
}
