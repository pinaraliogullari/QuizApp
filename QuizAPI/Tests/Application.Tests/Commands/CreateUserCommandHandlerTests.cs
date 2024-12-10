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
    private readonly CreateUserCommandHandler _createUserCommandHandler;
    public CreateUserCommandHandlerTests()
    {
        var userStoreMock = new Mock<IUserStore<AppUser>>();
        _userManagerMock = new Mock<UserManager<AppUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        _createUserCommandHandler = new CreateUserCommandHandler(_userManagerMock.Object);
    }

    [Fact]
    public async Task Handle_UserExistsByEmail_ReturnErrorResponse()
    {
        //Arrange

        var command = new CreateUserCommandRequest("testuser", "test@mail.com", "Sample1234.");
        var existingUser = new AppUser() { UserName = "testuser", Email = "test@mail.com" };
        _userManagerMock.Setup(x => x.FindByEmailAsync(command.Email)).ReturnsAsync(existingUser);

        //Act

        var result = await _createUserCommandHandler.Handle(command, default);

        //Assert

        result.IsSuccessful.Should().BeFalse();
        result.Message.Should().Be("This email address is already in use.");
    }
}
