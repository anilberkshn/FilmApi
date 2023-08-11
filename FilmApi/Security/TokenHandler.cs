using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FilmApi.Security
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }
     
        public string CreateAccessToken(string userName)
        {
        
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
 
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
 
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: DateTime.Now.AddHours(12),
                notBefore: DateTime.Now,
                claims: new []
                {
                    new Claim("userName", userName)
                },
                signingCredentials: signingCredentials
            );
            
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
 
            return tokenHandler.WriteToken(securityToken);
        }
    }
} 