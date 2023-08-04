using System.Threading.Tasks;
using FilmApi.Model.Entities;

namespace FilmApi.Clients
{
    public interface IOmdbHttpClient
    {
        public Task<FilmModel> GetCustomerByImdbId(string imdbId);
    }
}