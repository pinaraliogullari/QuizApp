using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizAPI.Application.Features.Commands.AppUser.CreateUser;
using QuizAPI.Application.Features.Commands.AppUser.LoginUser;
using QuizAPI.Application.Features.Commands.AppUser.UpdateUserScore;

namespace QuizAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginUserCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateScore(UpdateUserScoreCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);

    }
}
