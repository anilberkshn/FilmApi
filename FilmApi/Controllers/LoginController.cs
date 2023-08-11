using System.Collections.Generic;
using System.Net;
using Core.Model.ErrorModels;
using FilmApi.Model.RequestModels;
using FilmApi.Model.ResponseModels;
using FilmApi.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FilmApi.Controllers
{
    public class LoginController : Controller
    {
        readonly IConfiguration _configuration;

        public LoginController( IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("CreateToken")]
        public ActionResult Login([FromBody] LoginRequestModel model)
        {
            var userList = _configuration.GetSection("Users").Get<List<UserSettings>>();

            foreach (var user in userList)
            {
                if (model.Username == user.Username && model.Password == user.Password )//  && model.Roles ==user.Roles)
                {
                    TokenHandler tokenHandler = new TokenHandler(_configuration);
                    string token = tokenHandler.CreateAccessToken(model.Username);

                    return Ok(new LoginResponseModel
                    {
                        Success = true,
                        JwtToken = token
                    });
                }   
            }
            throw new CustomException(HttpStatusCode.Unauthorized,"User name or password error");
        }
    }
}