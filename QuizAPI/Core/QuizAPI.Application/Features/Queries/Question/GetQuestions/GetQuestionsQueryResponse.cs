namespace QuizAPI.Application.Features.Queries.Question.GetQuestions;

public record GetQuestionsQueryResponse(int Id, string InWords, string ImageName, List<string> Options);


