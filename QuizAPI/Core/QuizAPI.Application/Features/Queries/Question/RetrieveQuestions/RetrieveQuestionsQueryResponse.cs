namespace QuizAPI.Application.Features.Queries.Question.RetrieveQuestions;

public class RetrieveQuestionsQueryResponse
{
    public Guid Id { get; set; }
    public string InWords { get; set; }
    public string ImageName { get; set; }
    public List<string> Options { get; set; }
    public int Answer { get; set; }
}
