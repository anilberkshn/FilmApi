
namespace FilmApi.Model.ResponseModels
{
    public class LoginResponseModel
    {
        public bool Success { get; set; }
        public string JwtToken { get; set; }
    }
}