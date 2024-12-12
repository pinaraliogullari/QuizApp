using FluentValidation;
using QuizAPI.Application.Features.Commands.AppUser.LoginUser;

namespace QuizAPI.Application.Validators;

public class LoginUserCommandRequestValidator : AbstractValidator<LoginUserCommandRequest>
{
    public LoginUserCommandRequestValidator()
    {
        RuleFor(x => x.UserNameorEmail)
            .NotEmpty().WithMessage("UserName or Email is required");

        RuleFor(x => x.UserNameorEmail)
            .MinimumLength(4).WithMessage("UserName must be at least 4 characters")
            .When(x => !x.UserNameorEmail.Contains("@"));


        RuleFor(x => x.UserNameorEmail)
            .EmailAddress().WithMessage("Please enter a valid email address.")
            .When(x => x.UserNameorEmail.Contains("@"));


        RuleFor(x => x.Password)
            .NotEmpty().NotNull().WithMessage("Password is required")
            .MinimumLength(4).WithMessage("Password length must be at least 4 characters")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one digit")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character");
    }
}
