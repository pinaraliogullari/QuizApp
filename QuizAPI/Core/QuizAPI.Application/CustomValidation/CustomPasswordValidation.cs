using Microsoft.AspNetCore.Identity;
using QuizAPI.Domain.Entities;

namespace QuizAPI.Application.CustomValidation
{
    public class CustomPasswordValidation : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (password.ToLower().Contains(user.UserName.ToLower()))
                errors.Add(new IdentityError { Code = "PasswordContainsUserName", Description = "Please do not write your username in your password." });

            if (!errors.Any())
                return Task.FromResult(IdentityResult.Success);
            else
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
        }
    }
}
