using FluentValidation;

namespace BoilerPlate.Api.Controllers.Users.DTOS
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {

            RuleFor(x => x.Email)
             .NotEmpty()
             .EmailAddress()
             .WithMessage("valid Email Is Required");

            RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .WithMessage("Name must be at least 2 characters");

            RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters");
        }
    }
}
