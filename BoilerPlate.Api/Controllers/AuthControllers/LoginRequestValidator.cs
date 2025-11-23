using BoilerPlate.Application.Shared.Dtos.Auth;
using FluentValidation;

namespace BoilerPlate.Api.Controllers.AuthControllers
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {

            RuleFor(x => x.Email)
             .NotEmpty()
             .EmailAddress()
             .WithMessage("Invalid Email");
        }
    }
}
