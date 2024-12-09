namespace QuizAPI.Application.Features.Queries.Question.GetQuestions;

public record GetQuestionsQueryResponse(Guid Id, string InWords, string ImageName, List<string> Options);


