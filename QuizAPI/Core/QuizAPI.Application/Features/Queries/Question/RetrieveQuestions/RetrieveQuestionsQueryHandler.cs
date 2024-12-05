using MediatR;
using QuizAPI.Application.Repositories;

namespace QuizAPI.Application.Features.Queries.Question.RetrieveQuestions;

public class RetrieveQuestionsQueryHandler : IRequestHandler<RetrieveQuestionsQueryRequest, List<RetrieveQuestionsQueryResponse>>
{
    private readonly IQuestionReadRepository _questionReadRepository;

    public RetrieveQuestionsQueryHandler(IQuestionReadRepository questionReadRepository)
    {
        _questionReadRepository = questionReadRepository;
    }

    public async Task<List<RetrieveQuestionsQueryResponse>> Handle(RetrieveQuestionsQueryRequest request, CancellationToken cancellationToken)
    {
        var questions = await _questionReadRepository.GetAllAsync(options: null, include: null, tracking: false);
        var answers =  questions
            .Where(x => request.QuestionIds.Contains(x.Id))
            .Select(x => new RetrieveQuestionsQueryResponse()
            {
                Id = x.Id,
                InWords = x.InWords,
                ImageName = x.ImageName,
                Answer = x.Answer,
                Options = new List<string>
                {
                    x.Option1,
                    x.Option2,
                    x.Option3,
                    x.Option4
                }

            }).ToList();
        return answers;
    }
}

