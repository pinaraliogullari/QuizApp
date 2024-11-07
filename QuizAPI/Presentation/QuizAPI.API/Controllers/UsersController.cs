using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizAPI.Application.Features.Commands.AppUser.CreateUser;
using QuizAPI.Application.Features.Commands.AppUser.LoginUser;

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
    public async Task<IActionResult> CreateUser(CreateAppUserCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginAppUserCommandRequest request)
    {
        var response= await _mediator.Send(request);
        return Ok(response);
    }
}
