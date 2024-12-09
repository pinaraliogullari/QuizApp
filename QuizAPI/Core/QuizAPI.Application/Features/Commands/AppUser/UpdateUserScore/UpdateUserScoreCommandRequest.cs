using MediatR;

namespace QuizAPI.Application.Features.Commands.AppUser.UpdateUserScore;

public class UpdateUserScoreCommandRequest:IRequest<UpdateUserScoreCommandResponse>
{
    public Guid Id { get; set; }
    public int Score { get; set; }
    public int TimeTaken { get; set; }
}
