using MediatR;

namespace QuizAPI.Application.Features.Queries.Question.GetQuestions;

public class GetQuestionsQueryRequest : IRequest<List<GetQuestionsQueryResponse>>
{
}
