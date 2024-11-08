using QuizAPI.Application.DTOs;

namespace QuizAPI.Application;

public interface ITokenHandler
{
    Token CreateAccessToken(int minute);
}
