using FluentAssertions;
using QuizAPI.Application.Features.Commands.AppUser.CreateUser;
using QuizAPI.Application.Validators;

namespace QuizAPI.Application.Tests.Validators;


public class CreateUserCommandRequestValidatorTests
{
    private CreateUserCommandRequestValidator _validator;

    public CreateUserCommandRequestValidatorTests()
    {
        _validator = new CreateUserCommandRequestValidator();
    }

        [Fact]
        public async Task CreateUserValidator_WhenEmailIsNullOrEmpty_ShouldHaveValidationErrors()
        {
            // Arrange
            var request = new CreateUserCommandRequest("TestUser","", "Password1!");
       
            // Act
            var result = _validator.Validate(request);

            // Assert
            result.Errors.Should().Contain(x => x.PropertyName == "Email" && x.ErrorMessage == "Email is required");
        }

    [Fact]
    public async Task CreateUserValidator_WhenEmailIsInvalid_ShouldHaveValidationError()
    {
        // Arrange
        var request = new CreateUserCommandRequest("TestUser","invalid-email", "Password1!" );

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == "Email" && x.ErrorMessage == "Please enter a valid email address.");
    }

    //[Fact]
    //public async Task CreateUserValidator_WhenPasswordIsInvalid_ShouldHaveValidationError()
    //{
    //    // Arrange
    //    var command = new CreateUserCommandRequest
    //    {
    //        Email = "test@example.com",
    //        Password = "short",
    //        UserName = "TestUser"
    //    };

    //    // Act
    //    var result = _validator.Validate(command);

    //    // Assert
    //    result.Errors.Should().Contain(x => x.PropertyName == "Password" && x.ErrorMessage == "Password length must be at least 6 characters");
    //}

    //[Fact]
    //public async Task CreateUserValidator_WhenPasswordDoesNotContainUpperCase_ShouldHaveValidationError()
    //{
    //    // Arrange
    //    var command = new CreateUserCommandRequest
    //    {
    //        Email = "test@example.com",
    //        Password = "password1!",
    //        UserName = "TestUser"
    //    };

    //    // Act
    //    var result = _validator.Validate(command);

    //    // Assert
    //    result.Errors.Should().Contain(x => x.PropertyName == "Password" && x.ErrorMessage == "Password must contain at least one uppercase letter");
    //}

    //[Fact]
    //public async Task CreateUserValidator_WhenPasswordDoesNotContainSpecialCharacter_ShouldHaveValidationError()
    //{
    //    // Arrange
    //    var command = new CreateUserCommandRequest
    //    {
    //        Email = "test@example.com",
    //        Password = "Password123",
    //        UserName = "TestUser"
    //    };

    //    // Act
    //    var result = _validator.Validate(command);

    //    // Assert
    //    result.Errors.Should().Contain(x => x.PropertyName == "Password" && x.ErrorMessage == "Password must contain at least one special character");
    //}

    //[Fact]
    //public async Task CreateUserValidator_WhenUserNameIsTooShort_ShouldHaveValidationError()
    //{
    //    // Arrange
    //    var command = new CreateUserCommandRequest
    //    {
    //        Email = "test@example.com",
    //        Password = "Password1!",
    //        UserName = "Usr"
    //    };

    //    // Act
    //    var result = _validator.Validate(command);

    //    // Assert
    //    result.Errors.Should().Contain(x => x.PropertyName == "UserName" && x.ErrorMessage == "Username must be at least 4 characters long");
    //}

    //[Fact]
    //public async Task CreateUserValidator_WhenAllFieldsAreValid_ShouldNotHaveValidationErrors()
    //{
    //    // Arrange
    //    var command = new CreateUserCommandRequest("test@example.com", "Password1!", "TestUser");



    //    // Act
    //    var result = _validator.Validate(command);

    //    // Assert
    //    result.Errors.Should().BeEmpty();
    //}
}
