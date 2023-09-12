using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmApi.Controllers
{
    [Authorize(Roles="admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class JwtTestController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            string result = "Authentication is successful";
            return Ok(result);
        }
    }
}