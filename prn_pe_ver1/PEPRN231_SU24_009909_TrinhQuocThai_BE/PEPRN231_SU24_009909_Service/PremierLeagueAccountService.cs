using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PEPRN231_SU24_009909_Repo.Models;
using PEPRN231_SU24_009909_Repo.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PEPRN231_SU24_009909_Service
{
    public class PremierLeagueAccountService
    {
        private readonly IBaseRepository<PremierLeagueAccount> _repo;
        private readonly IConfiguration _configuration;
        public PremierLeagueAccountService(IBaseRepository<PremierLeagueAccount> repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }
        public async Task<string> Login(string email, string password)
        {
            PremierLeagueAccount? account = await _repo.Login(email, password);
            if (account == null)
            {
                return null; 
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"]
                    , _configuration["Jwt:Audience"]
                    , new Claim[]
                    {
                new(ClaimTypes.Email, account.EmailAddress),
                new(ClaimTypes.Role, account.Role.ToString()),
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

    }
}
