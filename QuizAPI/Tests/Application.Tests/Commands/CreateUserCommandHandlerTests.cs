using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using QuizAPI.Application.Features.Commands.AppUser.CreateUser;
using QuizAPI.Domain.Entities;
using Xunit;

namespace Application.Tests.Commands;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<UserManager<AppUser>> _userManagerMock;
    private readonly CreateUserCommandHandler _handler;
    public CreateUserCommandHandlerTests()
    {
        var userStoreMock = new Mock<IUserStore<AppUser>>();
        _userManagerMock = new Mock<UserManager<AppUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        _handler = new CreateUserCommandHandler(_userManagerMock.Object);
    }

    [Fact]
    public async Task Handle_UserExistsByEmail_ShouldReturnErrorResponse()
    {
        //Arrange
        var command = new CreateUserCommandRequest("testuser", "test@mail.com", "Sample1234.");
        var existingUser = new AppUser() { UserName = "olduser", Email = "test@mail.com" };
        _userManagerMock.Setup(x => x.FindByEmailAsync(command.Email)).ReturnsAsync(existingUser);

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        result.IsSuccessful.Should().BeFalse();
        result.Message.Should().Be("This email address is already in use.");
    }

    [Fact]
    public async Task Handle_UserExistsByUsername_ShouldReturnErrorResponse()
    {
        //Arrange
        var command = new CreateUserCommandRequest( "testuser",  "test@mail.com","Sample1234.");
        var existingUser = new AppUser() { UserName = "testuser", Email = "olduser@mail.com" };
        _userManagerMock.Setup(x=>x.FindByNameAsync(command.UserName)).ReturnsAsync(existingUser);

        //Act
        var result= await _handler.Handle(command,default);

        //Assert
        result.IsSuccessful.Should().BeFalse();
        result.Message.Should().Be("This username is already taken.");

     
    }

    [Fact]
    public async Task Handle_UserCreatedSuccessfully_ShouldReturnSuccessResponse()
    {
        //Arrange
        var command = new CreateUserCommandRequest("newuser", "newuser@mail.com", "Newpassword1234.");
        _userManagerMock.Setup(x => x.FindByEmailAsync(command.Email)).ReturnsAsync((AppUser)null);
        _userManagerMock.Setup(x => x.FindByNameAsync(command.UserName)).ReturnsAsync((AppUser)null);
        _userManagerMock.Setup(x=>x.CreateAsync(It.IsAny<AppUser>(),command.Password)).ReturnsAsync(IdentityResult.Success);

        //Act
        var result= await _handler.Handle(command,default);

        //Assert
        result.IsSuccessful.Should().BeTrue();
        result.Message.Should().Be("User has been created successfully.");
    }

    [Fact]
    public async Task Handle_UserCreationFailed_ShouldReturnErrorResponseWithErrors()
    {
        //Arrange
        var command = new CreateUserCommandRequest("newuser", "newuser@mail.com", "Newpassword1234.");
        _userManagerMock.Setup(x => x.FindByEmailAsync(command.Email)).ReturnsAsync((AppUser)null);
        _userManagerMock.Setup(x => x.FindByNameAsync(command.UserName)).ReturnsAsync((AppUser)null);
        var identityErrors = new List<IdentityError>()
        {
            new IdentityError { Code = "PasswordTooShort", Description = "The password is too short." },
            new IdentityError { Code = "DuplicateUserName", Description = "The username is already taken." }
        };
        var failedResult = IdentityResult.Failed(identityErrors.ToArray());
        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(failedResult);

        //Act
        var result= await _handler.Handle(command, default);

        //Assert
        result.IsSuccessful.Should().BeFalse();
        result.Message.Should().Contain("User creation failed. Errors:\n");
        result.Message.Should().Contain("PasswordTooShort: The password is too short.");
        result.Message.Should().Contain("DuplicateUserName: The username is already taken.");
    }
}
