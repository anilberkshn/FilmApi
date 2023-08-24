using System.Collections.Generic;
using System.Threading.Tasks;
using FilmApi.Model.Entities;
using FilmApi.Model.RequestModels;

namespace FilmApi.Clients
{
    public interface IOmdbHttpClient
    {
        public Task<FilmModel> GetByImdbId(string imdbId);
        public Task<IEnumerable<FilmModel>> GetByTitle(string imdbId);
    }
}