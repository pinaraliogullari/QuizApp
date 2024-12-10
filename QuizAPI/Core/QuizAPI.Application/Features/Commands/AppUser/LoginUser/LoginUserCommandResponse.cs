using QuizAPI.Application.DTOs;

namespace QuizAPI.Application.Features.Commands.AppUser.LoginUser;

public record LoginUserCommandResponse(bool IsSuccessful, string? Message, Token? Token);

