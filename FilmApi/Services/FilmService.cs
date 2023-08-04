using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Core.Model.ErrorModels;
using Core.Model.RequestModel;
using FilmApi.Clients;
using FilmApi.Model.Entities;
using FilmApi.Model.RequestModels;
using FilmApi.Repository;

namespace FilmApi.Services
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IOmdbHttpClient _omdbHttpClient;

        public FilmService(IFilmRepository filmRepository, IOmdbHttpClient omdbHttpClient)
        {
            _filmRepository = filmRepository;
            _omdbHttpClient = omdbHttpClient;
        }

        public async Task<FilmModel> InsertAsync(FilmModel filmModel)
        {
            //todo : http client ile önce bu imdb id de varmı kontrol edilip yoksa eklemeyi kafamıza göre yapamamamız lazım gibi.Var olursa o imdbId li veriler dönmek lazım. 

            return await _filmRepository.InsertAsync(filmModel);
        }

        public async Task<IEnumerable<FilmModel>> GetAllAsync()
        {
            return await _filmRepository.GetAllAsync();
        }

        public async Task<FilmModel> GetByIdAsync(string id)
        {
            var film = await _filmRepository.GetByIdAsync(id);

            if (film == null)
            {
                var httpResponse = await _omdbHttpClient.GetCustomerByImdbId(id);
                if (httpResponse == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, "Bu id de film bulunamadı.");
                }
                else
                {
                    await _filmRepository.InsertAsync(httpResponse);
                    return httpResponse;
                }
            }

            if (film.IsDeleted)
            {
                throw new CustomException(HttpStatusCode.NotFound, "Film  veri tabanımızda bulunamadı.");
            }

            return film;
        }

        public async Task<IEnumerable<FilmModel>> GetAllSkipTakeAsync(GetAllDto getAllDto)
        {
            return await _filmRepository.GetAllSkipTakeAsync(getAllDto);
        }

        public async Task<FilmModel> Update(string id, UpdateDto updateDto)
        {
            var result = await _filmRepository.Update(id, updateDto);
            // Kendi veritabanımızda film bilgisini güncelleyebilmek için 
            return result;
        }

        public void Delete(string id)
        {
            _filmRepository.DeleteRepo(id);
        }

        public async Task<IEnumerable<FilmModel>> GetByTitleRepoAsync(SearchByTitleDto byTitleDto)
        {
            var films = await _filmRepository.GetByTitleRepoAsync(byTitleDto);
            // todo : Filmler veri tabanımızda yoksa Http client ile isteyip veri tabanımıza kaydetmeli. httpclientda da yoksa film bulunamadı. 
            return films;
        }
    }
}