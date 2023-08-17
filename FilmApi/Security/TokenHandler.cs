using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using Core.Model.ErrorModels;
using FilmApi.Model.RequestModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FilmApi.Security
{
    public class TokenHandler
    {
        private IConfiguration Configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }
     
        public string CreateAccessToken(string userName)
        {
        
            List<LoginRequestModel> users = Configuration.GetSection("Users").Get<List<LoginRequestModel>>();
            LoginRequestModel user = users.FirstOrDefault(u => u.Username == userName);
            if (user == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, "The user was not found");
            }

            string[] roles = user.Roles.Split(',');
            
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
 
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
 
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: DateTime.Now.AddHours(12),
                notBefore: DateTime.Now,
                claims: roles.Select(role => new Claim(ClaimTypes.Role,role))
                    .Concat(new Claim[]
                    {
                        new Claim("userName", userName)
                    }),
               
                signingCredentials: signingCredentials
            );
            
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
 
            return tokenHandler.WriteToken(securityToken);
        }
    }
} 