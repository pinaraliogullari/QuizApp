﻿using FluentAssertions;
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

    [Fact]
    public async Task CreateUserValidator_WhenPasswordIsTooShort_ShouldHaveValidationError()
    {
        // Arrange
        var request = new CreateUserCommandRequest("TestUser", "test@example.com", "short");
      
        // Act
        var result = _validator.Validate(request);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == "Password" && x.ErrorMessage == "Password length must be at least 6 characters");
   
    }
    [Fact]
    public async Task CreateUserValidator_WhenPasswordDoesNotContainUpperCase_ShouldHaveValidationError()
    {
        //Arrange
        var request = new CreateUserCommandRequest("TestUser", "test@example.com", "short123.");

        //Act
        var result = _validator.Validate(request);

        //Assert
        result.Errors.Should().Contain(x => x.PropertyName == "Password" && x.ErrorMessage == "Password must contain at least one uppercase letter");
    }

    [Fact]

    public async Task CreateUserValidator_WhenPasswordDoesNotContainLowerCase_ShouldHaveValidationError()
    {
        //Arrange
        var request = new CreateUserCommandRequest("TestUser", "test@example.com", "SHORT123.");

        //Act
        var result = _validator.Validate(request);

        //Assert
        result.Errors.Should().Contain(x => x.PropertyName == "Password" && x.ErrorMessage == "Password must contain at least one lowercase letter");
    }

    [Fact]
    public async Task CreateUserValidator_WhenPasswordDoesNotContainSpecialCharacter_ShouldHaveValidationError()
    {
        //Arrange
        var request = new CreateUserCommandRequest("TestUser", "test@example.com", "Short123");

        //Act
        var result = _validator.Validate(request);

        //Assert
        result.Errors.Should().Contain(x => x.PropertyName == "Password" && x.ErrorMessage == "Password must contain at least one special character");
    }

    [Fact]
    public async Task CreateUserValidator_WhenPasswordDoesNotContainDigit_ShouldHaveValidationError()
    {
        //Arrange
        var request = new CreateUserCommandRequest("TestUser", "test@example.com", "Short/");

        //Act
        var result = _validator.Validate(request);

        //Assert
        result.Errors.Should().Contain(x => x.PropertyName == "Password" && x.ErrorMessage == "Password must contain at least one digit");
    }
    [Fact]
    public async Task CreateUserValidator_WhenUserNameIsNullOrEmpty_ShouldHaveValidationErrors()
    {
        // Arrange
        var request = new CreateUserCommandRequest("", "test@mail.com", "Password1!");

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == "UserName" && x.ErrorMessage == "Username is required");
    }
    [Fact]
    public async Task CreateUserValidator_WhenUserNameIsTooShort_ShouldHaveValidationErrors()
    {
        // Arrange
        var request = new CreateUserCommandRequest("abc", "test@mail.com", "Password1!");

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == "UserName" && x.ErrorMessage == "Username must be at least 4 characters long");
    }
    [Fact]
    public async Task CreateUserValidator_WhenAllFieldsAreValid_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var request = new CreateUserCommandRequest("TestUser","test@example.com", "Password1!" );

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.Errors.Should().BeEmpty();
    }


}
