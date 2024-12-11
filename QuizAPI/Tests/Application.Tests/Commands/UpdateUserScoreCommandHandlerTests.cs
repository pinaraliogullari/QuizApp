﻿using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using QuizAPI.Application.Features.Commands.AppUser.UpdateUserScore;
using QuizAPI.Domain.Entities;
using Xunit;

namespace Application.Tests.Commands
{
    public class UpdateUserScoreCommandHandlerTests
    {
        private readonly Mock<UserManager<AppUser>> _mockUserManager;
        private readonly UpdateUserScoreCommandHandler _handler;
        public UpdateUserScoreCommandHandlerTests()
        {
            var userStoreMock = new Mock<IUserStore<AppUser>>();
            _mockUserManager = new Mock<UserManager<AppUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            _handler = new UpdateUserScoreCommandHandler(_mockUserManager.Object);
        }

        [Fact]
        public async Task Handle_UserNotFound_ShouldThrowArgumentNullException()
        {
            //Arrange
            var request = new UpdateUserScoreCommandRequest() { Id = Guid.NewGuid(), Score = 5, TimeTaken = 15 };
            _mockUserManager.Setup(x => x.FindByIdAsync(request.Id.ToString())).ReturnsAsync((AppUser)null);

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ScoreUpdatedSuccessfully_ShouldReturnSuccessResponse()
        {
            //Arrange
            var request = new UpdateUserScoreCommandRequest() {Id= Guid.NewGuid(), Score = 5, TimeTaken = 15 };
            var user= new AppUser() { Id= Guid.NewGuid().ToString(), Score = 4,TimeTaken = 20 };
            _mockUserManager.Setup(x=>x.FindByIdAsync(request.Id.ToString())).ReturnsAsync((user));
            user.Score=request.Score;
            user.TimeTaken=request.TimeTaken;
            _mockUserManager.Setup(x=>x.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);

            //Act
            var response= await _handler.Handle(request,CancellationToken.None);

            //Assert
            response.Success.Should().BeTrue();
            response.Message.Should().Be("User score updated successfully");
            user.Score.Should().Be(request.Score);
            user.TimeTaken.Should().Be(request.TimeTaken);
        }
    }
}
