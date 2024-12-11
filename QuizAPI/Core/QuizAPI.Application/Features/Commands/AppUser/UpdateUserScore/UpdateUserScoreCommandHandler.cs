using MediatR;
using Microsoft.AspNetCore.Identity;

namespace QuizAPI.Application.Features.Commands.AppUser.UpdateUserScore;

public class UpdateUserScoreCommandHandler : IRequestHandler<UpdateUserScoreCommandRequest, UpdateUserScoreCommandResponse>
{
    private readonly UserManager<Domain.Entities.AppUser> _userManager;

    public UpdateUserScoreCommandHandler(UserManager<Domain.Entities.AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UpdateUserScoreCommandResponse> Handle(UpdateUserScoreCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user == null)
            throw new ArgumentNullException("User can not found");
        user.Score = request.Score;
        user.TimeTaken = request.TimeTaken;
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return new UpdateUserScoreCommandResponse
            {
                Message = "User score updated successfully",
                Success = true,
            };
        }
        else
        {
            return new UpdateUserScoreCommandResponse
            {
                Success = false,
                Message = "Failed to update user score"
            };
        }


        throw new NotImplementedException();
    }
}
