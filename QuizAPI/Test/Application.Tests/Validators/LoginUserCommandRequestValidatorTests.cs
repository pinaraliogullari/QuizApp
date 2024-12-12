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
        public async Task LoginUserValidate_WhenUserNameOrEmailIsEmpty_ShouldHaveError()
        {
            //Arrange
            var request = new LoginUserCommandRequest("", "Validpassword123.");

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.UserNameorEmail)
                  .WithErrorMessage("UserName or Email is required");
        }

        [Fact]
        public async Task LoginUserValidate_WhenUserNameIsLessThan4Characters_ShouldHaveError()
        {
            //Arrange
            var request = new LoginUserCommandRequest("abc", "ValidPassword1!");

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.UserNameorEmail)
                  .WithErrorMessage("UserName must be at least 4 characters");
        }



    }
}
