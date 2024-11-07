using MediatR;

namespace QuizAPI.Application.Features.Commands.AppUser.LoginUser;

public record LoginAppUserCommandRequest(string UserNameorEmail, string Password):IRequest<LoginAppUserCommandResponse>;

