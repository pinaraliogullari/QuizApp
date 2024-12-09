using MediatR;
using Microsoft.AspNetCore.Identity;
using QuizAPI.Application.DTOs;

namespace QuizAPI.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    private readonly UserManager<Domain.Entities.AppUser> _userManager;
    private readonly SignInManager<Domain.Entities.AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;

    public LoginUserCommandHandler(
        UserManager<Domain.Entities.AppUser> userManager,
        SignInManager<Domain.Entities.AppUser> signInManager,
        ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        LoginUserCommandResponse response = new(false, "", null);
        Domain.Entities.AppUser user = await _userManager.FindByEmailAsync(request.UserNameorEmail);
        if (user == null)
            user = await _userManager.FindByNameAsync(request.UserNameorEmail);
        if (user == null)
            response = response with { IsSuccessful = false, Message = "Username or password is not correct", Token = null };

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (result.Succeeded)//Authentication success.
        {
            var userId = user.Id.ToString();
            var userName = user.UserName;
            Token token = _tokenHandler.CreateAccessToken(60, userId,userName);
            response = response with { IsSuccessful = true, Message = "Login is successful", Token = token };
            //Authorization process...

        }
        else
            response = response with { IsSuccessful = false, Message = "Authentication error!", Token = null };
        return response;

    }
}
