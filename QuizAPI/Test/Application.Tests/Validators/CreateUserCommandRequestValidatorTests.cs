using FluentAssertions;
using QuizAPI.Application.Features.Commands.AppUser.CreateUser;
using QuizAPI.Application.Validators;

namespace Application.Tests.Validators;

public class CreateUserCommandRequestValidatorTests
{
    private readonly CreateUserCommandRequestValidator _validator;
    public CreateUserCommandRequestValidatorTests()
    {
        _validator = new CreateUserCommandRequestValidator();
    }

    [Fact]
    public async Task CreateUserValidator_WhenEmailIsNullOrEmpty_ShouldHaveValidationError()
    {
        //Arrange
        var request = new CreateUserCommandRequest("testuser", "", "testpassword");
                
        //Act
        var result= _validator.Validate(request);

        //Assert
        result.Errors.Should().Contain(x => x.PropertyName == "Email" && x.ErrorMessage == "Email is required");
    }

}
