using MediatR;

namespace QuizAPI.Application.Features.Queries.Question.RetrieveQuestions;

public class RetrieveQuestionsQueryRequest:IRequest<List<RetrieveQuestionsQueryResponse>>
{
    public List<Guid> QuestionIds { get; set; }
}
