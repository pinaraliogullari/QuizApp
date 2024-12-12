using FluentValidation.TestHelper;
using QuizAPI.Application.Features.Commands.AppUser.LoginUser;

namespace QuizAPI.Application.Validators.Tests
{
    public class LoginUserCommandRequestValidatorTests
    {
        private readonly LoginUserCommandRequestValidator _validator;

        public LoginUserCommandRequestValidatorTests()
        {
            _validator = new LoginUserCommandRequestValidator();
        }

        [Fact]
        public void LoginUserValidate_WhenUserNameOrEmailIsEmpty_ShouldHaveError()
        {
            //Arrange
            var request = new LoginUserCommandRequest("", "ValidPassword1!");

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.UserNameorEmail)
                  .WithErrorMessage("UserName or Email is required");
        }

    }
}
