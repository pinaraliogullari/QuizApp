using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizAPI.Application;
using QuizAPI.Application.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace QuizAPI.Infrastructure
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration configuration;

        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Token CreateAccessToken(int minute)
        {
            Token token = new Token();

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials=new(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

            token.Expiration=DateTime.UtcNow.AddMinutes(minute);

            JwtSecurityToken jwtSecurityToken = new
                (
                audience: configuration["Token:Audience"],
                issuer: configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
                );
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            token.AccessToken=jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
