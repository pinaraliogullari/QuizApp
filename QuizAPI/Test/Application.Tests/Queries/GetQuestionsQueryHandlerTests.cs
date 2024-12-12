using FluentAssertions;
using Moq;
using QuizAPI.Application.Features.Queries.Question.GetQuestions;
using QuizAPI.Application.Repositories;
using QuizAPI.Domain.Entities;

namespace Application.Tests.Queries;

public class GetQuestionsQueryHandlerTests
{
    private readonly Mock<IQuestionReadRepository> _mockQuestionReadRepository;
    private readonly GetQuestionsQueryHandler _handler;
    public GetQuestionsQueryHandlerTests()
    {
        _mockQuestionReadRepository = new Mock<IQuestionReadRepository>();
        _handler = new GetQuestionsQueryHandler(_mockQuestionReadRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenQuestionsExist_ShouldReturnRandom5Questions()
    {
        //Arrange
        var questions = new List<Question>()
        {
            new Question() { Id = Guid.NewGuid(), InWords = "Question 1", ImageName = "image1.png", Option1 = "A", Option2 = "B", Option3 = "C", Option4 = "D" },
            new Question() { Id = Guid.NewGuid(), InWords = "Question 2", ImageName = "image2.png", Option1 = "E", Option2 = "F", Option3 = "G", Option4 = "H" },
            new Question() { Id = Guid.NewGuid(), InWords = "Question 3", ImageName = "image3.png", Option1 = "I", Option2 = "J", Option3 = "K", Option4 = "L" },
            new Question() { Id = Guid.NewGuid(), InWords = "Question 4", ImageName = "image4.png", Option1 = "M", Option2 = "N", Option3 = "O", Option4 = "P" },
            new Question() { Id = Guid.NewGuid(), InWords = "Question 5", ImageName = "image5.png", Option1 = "Q", Option2 = "R", Option3 = "S", Option4 = "T" },

        };
        var mappedQuestions = questions.Select(q => new GetQuestionsQueryResponse(
         q.Id,
         q.InWords,
         q.ImageName,
         new List<string> { q.Option1, q.Option2, q.Option3, q.Option4 }
         )).ToList();
        _mockQuestionReadRepository.Setup(x => x.GetAllAsync(null, null, false)).ReturnsAsync(questions);
        var request = new GetQuestionsQueryRequest();

        //Act
        var response = await _handler.Handle(request, CancellationToken.None);

        //Assert
        response.Should().NotBeNullOrEmpty();
        response.Count().Should().Be(5);
        response.Should().OnlyHaveUniqueItems(x => x.Id);
     
        response.Should().BeEquivalentTo(mappedQuestions, options => options.WithoutStrictOrdering());
    }

    [Fact]
    public async Task Handle_WhenQuestionsAreNotExist_ShouldThrowInvalidOperationException()
    {
        //Arrange
        var request = new GetQuestionsQueryRequest();
        _mockQuestionReadRepository.Setup(x => x.GetAllAsync(null, null, false)).ReturnsAsync((List<Question>)null);

        //Act 
        Func<Task> action = async () => await _handler.Handle(request, CancellationToken.None);

        //Assert
        await action.Should().ThrowAsync<InvalidOperationException>().WithMessage("No questions available to retrieve.");

    }
}
