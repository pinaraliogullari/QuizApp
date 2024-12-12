using FluentAssertions;
using Moq;
using QuizAPI.Application.Features.Queries.Question.RetrieveQuestions;
using QuizAPI.Application.Repositories;
using QuizAPI.Domain.Entities;

namespace Application.Tests.Queries;

public class RetrieveQuestionsQueryHandlerTests
{
    private readonly Mock<IQuestionReadRepository> _mockQuestionReadRepository;
    private readonly RetrieveQuestionsQueryHandler _handler;
    public RetrieveQuestionsQueryHandlerTests()
    {
        _mockQuestionReadRepository = new Mock<IQuestionReadRepository>();
        _handler = new RetrieveQuestionsQueryHandler(_mockQuestionReadRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidIds_ShouldReturnQuestions()
    {
        //Arrange
        var request = new RetrieveQuestionsQueryRequest()
        {
            QuestionIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid()}
        };
        var questions = new List<Question>
        {
            new Question
            {
                Id = request.QuestionIds[0],
                InWords = "What is C#?",
                ImageName = "csharp.jpg",
                Answer = 1,
                Option1 = "A tool",
                Option2 = "A programming language",
                Option3 = "An operating system",
                Option4 = "A framework"
            },
            new Question
            {
                Id = request.QuestionIds[1],
                InWords = "What is .NET?",
                ImageName = "dotnet.jpg",
                Answer = 3,
                Option1 = ".NET framework",
                Option2 = "A programming language",
                Option3 = "A database",
                Option4 = "An IDE"
            }
        };
        _mockQuestionReadRepository.Setup(x=>x.GetAllAsync(null,null,false)).ReturnsAsync(questions);

        //Act
        var response= await _handler.Handle(request,CancellationToken.None);

        //Assert
        response.Should().NotBeNullOrEmpty();
        response.Should().HaveCount(2);
        response.Should().Contain(x => x.Id == request.QuestionIds[0]);
        response.Should().Contain(x=>x.Id== request.QuestionIds[1]);

    }
}
