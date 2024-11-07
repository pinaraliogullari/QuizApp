using Microsoft.AspNetCore.Identity;

namespace QuizAPI.Application.CustomValidation;

public class CustomIdentityErrorDescriber:IdentityErrorDescriber
{
    public override IdentityError InvalidEmail(string email) => new IdentityError { Code = "InvalidEmail", Description = "Invalid email." };
    public override IdentityError DuplicateEmail(string email) => new IdentityError { Code = "DuplicateEmail", Description = $"\"{email}\" is in use by another user." };
    public override IdentityError InvalidUserName(string userName) => new IdentityError { Code = "InvalidUserName", Description = "Invalid Username" };
    public override IdentityError DuplicateUserName(string userName) => new IdentityError { Code = "DuplicateUserName", Description = $"\"{userName}\" is in use by another user." };
}
