using MediatR;

namespace QuizAPI.Application.Features.Commands.AppUser.CreateUser;

public record CreateUserCommandRequest(string UserName,string Email,string Password) : IRequest<CreateUserCommandResponse>;
