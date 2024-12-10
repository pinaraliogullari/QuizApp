using MediatR;

namespace QuizAPI.Application.Features.Commands.AppUser.LoginUser;

public record LoginUserCommandRequest(string UserNameorEmail, string Password) : IRequest<LoginUserCommandResponse>;

