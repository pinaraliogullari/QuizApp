using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizAPI.Application;
using QuizAPI.Application.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuizAPI.Infrastructure
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateAccessToken(int minute, string userId, string userName)
        {
            Token token = new Token();

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.AddMinutes(minute);
            var claims = new[]
               {
                new Claim("userId", userId), 
                new Claim("userName", userName) 
            };



            JwtSecurityToken jwtSecurityToken = new
                (
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                claims: claims,
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
                );
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
