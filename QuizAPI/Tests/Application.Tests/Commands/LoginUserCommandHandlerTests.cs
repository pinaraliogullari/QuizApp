using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using QuizAPI.Application;
using QuizAPI.Application.DTOs;
using QuizAPI.Application.Features.Commands.AppUser.LoginUser;
using QuizAPI.Domain.Entities;
using Xunit;

namespace Application.Tests.Commands;

public class LoginUserCommandHandlerTests
{
    private readonly Mock<UserManager<AppUser>> _mockUserManager;
    private readonly Mock<SignInManager<AppUser>> _mockSignInManager;
    private readonly Mock<ITokenHandler> _mockTokenHandler;
    private readonly LoginUserCommandHandler _handler;

    public LoginUserCommandHandlerTests()
    {
        _mockUserManager = new Mock<UserManager<AppUser>>(
            Mock.Of<IUserStore<AppUser>>(),
            null, null, null, null, null, null, null, null);

        _mockSignInManager = new Mock<SignInManager<AppUser>>(
            _mockUserManager.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<AppUser>>(),
            null, null, null, null);

        _mockTokenHandler = new Mock<ITokenHandler>();

        _handler = new LoginUserCommandHandler(
            _mockUserManager.Object,
            _mockSignInManager.Object,
            _mockTokenHandler.Object);
    }
    [Fact]

    public async Task Handle_UserNotFound_ShouldReturnErrorResponse()
    {
        // Arrange
        var request = new LoginUserCommandRequest(UserNameorEmail: "nonexistent@mail.com", Password: "Testpassword123.");
        _mockUserManager.Setup(x => x.FindByEmailAsync(request.UserNameorEmail)).ReturnsAsync((AppUser)null);
        _mockUserManager.Setup(x => x.FindByNameAsync(request.UserNameorEmail)).ReturnsAsync((AppUser)null);

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.IsSuccessful.Should().BeFalse();
        response.Message.Should().Be("Username or password is not correct");
        response.Token.Should().BeNull();
    }

    [Fact]
    public async Task Handle_LoginIsFail_ShouldReturnErrorResponse()
    {
        //Arrange
        var request = new LoginUserCommandRequest(UserNameorEmail: "existuser@mail.com", Password: "Testpassword123.");
        var user = new AppUser() { Email = "existuser@mail.com", UserName = "existuser" };
       _mockUserManager.Setup(x => x.FindByEmailAsync(request.UserNameorEmail)).ReturnsAsync(user);
        _mockSignInManager.Setup(x=>x.CheckPasswordSignInAsync(It.IsAny<AppUser>(),request.Password,false)).ReturnsAsync(SignInResult.Failed);

        //Act
        var response= await _handler.Handle(request,CancellationToken.None);

        //Assert
        response.IsSuccessful.Should().BeFalse();
        response.Message.Should().Be("Authentication error!");
        response.Token.Should().BeNull();
    }

    [Fact]
    public async Task Handle_LoginIsSuccess_ShouldReturnSuccessResponse()
    {
        //Arrange
        var request = new LoginUserCommandRequest(UserNameorEmail: "existuser@mail.com", Password: "Testpassword123.");
        var user = new AppUser() {Id=Guid.NewGuid().ToString(), Email = "existuser@mail.com", UserName = "existuser" };
        var token = new Token { AccessToken = "sampleAccessToken", Expiration = System.DateTime.UtcNow.AddMinutes(60) };

        _mockUserManager.Setup(x => x.FindByEmailAsync(request.UserNameorEmail)).ReturnsAsync(user);
        _mockSignInManager.Setup(x => x.CheckPasswordSignInAsync(It.IsAny<AppUser>(), request.Password, false)).ReturnsAsync(SignInResult.Success);
        _mockTokenHandler.Setup(x => x.CreateAccessToken(It.IsAny<int>(), user.Id, It.IsAny<string>())).Returns(token);

        //Act
        var response = await _handler.Handle(request, CancellationToken.None);

        //Assert
        response.IsSuccessful.Should().BeTrue();
        response.Message.Should().Be("Login is successful");
        response.Token.Should().BeEquivalentTo(token);

    }


}
