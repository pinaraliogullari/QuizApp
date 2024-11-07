using MediatR;
using Microsoft.AspNetCore.Identity;

namespace QuizAPI.Application.Features.Commands.AppUser.LoginUser;

public class LoginAppUserCommandHandler : IRequestHandler<LoginAppUserCommandRequest, LoginAppUserCommandResponse>
{
    private readonly UserManager<Domain.Entities.AppUser> _userManager;
    private readonly SignInManager<Domain.Entities.AppUser> _signInManager;

    public LoginAppUserCommandHandler(
        UserManager<Domain.Entities.AppUser> userManager,
        SignInManager<Domain.Entities.AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<LoginAppUserCommandResponse> Handle(LoginAppUserCommandRequest request, CancellationToken cancellationToken)
    {
        LoginAppUserCommandResponse response = new(false, "");
        Domain.Entities.AppUser user = await _userManager.FindByEmailAsync(request.UserNameorEmail);
        if (user == null)
            user = await _userManager.FindByNameAsync(request.UserNameorEmail);
        if (user == null)
            response = response with { IsSuccessful = false, Message = "Username or password is not correct" };

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (result.Succeeded)//Authentication success.
        {
            //Authorization process...

        }
        response = response with { IsSuccessful = false, Message = "Authentication error!" };
        return response;

    }
}
