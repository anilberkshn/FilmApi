namespace FilmApi.Model.RequestModels
{
    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}