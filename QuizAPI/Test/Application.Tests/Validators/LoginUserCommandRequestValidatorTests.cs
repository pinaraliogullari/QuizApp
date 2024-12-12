using FluentAssertions;
using FluentValidation.TestHelper;
using QuizAPI.Application.Features.Commands.AppUser.CreateUser;
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

        [Fact]
        public async Task LoginUserValidate_WhenEmailIsInvalid_ShouldHaveError()
        {
            //Arrange
            var request = new LoginUserCommandRequest("@xy", "ValidPassword1!");

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.UserNameorEmail)
                  .WithErrorMessage("Please enter a valid email address.");
        }
        [Fact]
        public async Task LoginUserValidate_WhenAllFieldsAreValid_ShouldNotHaveError()
        {
            // Arrange
            var request = new LoginUserCommandRequest("TestUser", "ValidPassword1!");

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.UserNameorEmail);
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
        }
        [Fact]
        public async Task LoginUserValidate_WhenPasswordIsEmpty_ShouldHaveError()
        {
            // Arrange
            var request = new LoginUserCommandRequest("TestUser", "");

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage("Password is required");
        }
        [Fact]
        public async Task LoginUserValidate_WhenPasswordIsLessThan4Characters_ShouldHaveError()
        {
            // Arrange
            var request = new LoginUserCommandRequest("TestUser", "abc");

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage("Password length must be at least 4 characters");
        }

    }
}
