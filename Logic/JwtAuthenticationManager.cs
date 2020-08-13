using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using expense_tracker.Repositories;
using Microsoft.Extensions.Configuration;

namespace expense_tracker.Logic
{
    public interface IJwtAuthenticationManager
    {
        Task<string> Authenticate(string username, string password);
    }

    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IConfiguration _configuration;

        public JwtAuthenticationManager(
            IConfiguration configuration, 
            IUsersRepository usersRepository)
        {
            _configuration = configuration;
            _usersRepository = usersRepository;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            var user = (await _usersRepository.SearchUser(username)).FirstOrDefault();

            if (user == null || user.PasswordHash != UserHelper.GetHashedPassword(password + user.Salt, password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            { 
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Issuer"],
                IssuedAt = DateTime.UtcNow,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("UserId", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
