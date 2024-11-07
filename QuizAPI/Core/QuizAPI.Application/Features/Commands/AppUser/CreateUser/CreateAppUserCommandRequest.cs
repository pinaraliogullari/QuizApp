using MediatR;

namespace QuizAPI.Application.Features.Commands.AppUser.CreateUser;

public record CreateAppUserCommandRequest(string UserName,string Email,string Password) : IRequest<CreateAppUserCommandResponse>;
