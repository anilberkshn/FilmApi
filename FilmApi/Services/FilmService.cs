using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Core.Model.ErrorModels;
using Core.Model.RequestModel;
using FilmApi.Clients;
using FilmApi.Helper.Mapper;
using FilmApi.Model.Entities;
using FilmApi.Model.RequestModels;
using FilmApi.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace FilmApi.Services
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IOmdbHttpClient _omdbHttpClient;
        private readonly IMemoryCache _memoryCache;

        public FilmService(IFilmRepository filmRepository, IOmdbHttpClient omdbHttpClient, IMemoryCache memoryCache)
        {
            _filmRepository = filmRepository;
            _omdbHttpClient = omdbHttpClient;
            _memoryCache = memoryCache;
        }
  
        public async Task<FilmModel> InsertAsync(FilmModel filmModel)
        {
            return await _filmRepository.InsertAsync(filmModel);
        }

        public async Task<IEnumerable<FilmModel>> GetAllAsync()
        {
            return await _filmRepository.GetAllAsync();
        }

        public async Task<FilmModel> GetByIdAsync(string id)
        {
            if (_memoryCache.TryGetValue(id, out FilmModel cachedFilm))
            {
                return cachedFilm;
            }
            
            var film = await _filmRepository.GetByIdAsync(id);
            _memoryCache.Set(id, film, TimeSpan.FromDays(30));
            if (film == null)
            {
                var httpResponse = await _omdbHttpClient.GetByImdbId(id);
                if (httpResponse == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, "This id also could not find the movie.");
                }
                else
                {
                    _memoryCache.Set(id, httpResponse, TimeSpan.FromDays(30));
                    await _filmRepository.InsertAsync(httpResponse);
                    return httpResponse;
                }
            }

            if (film.IsDeleted)
            {
                throw new CustomException(HttpStatusCode.NotFound, "This id was also not found in the movie database.");
            }

            return film;
        }
        
        public async Task<IEnumerable<SearchByTitleDto>> GetByTitleAsync(string byTitleDto)
        {
            var films = await _filmRepository.GetByTitleAsync(byTitleDto);

            List<SearchByTitleDto> listToSearchDto = null;
            
            foreach (var film in films)
            {
                SearchByTitleDto toSearchDto = await FilmToSearchTitleDto.MapToSearchByTitleDto(film);
                listToSearchDto.Add(toSearchDto);
            }
            
            
            
            // todo : Filmler veri tabanımızda yoksa Http client ile isteyip veri tabanımıza kaydetmeli. httpclientda da yoksa film bulunamadı. 
            return listToSearchDto;
        }

        public async Task<IEnumerable<FilmModel>> GetAllSkipTakeAsync(GetAllDto getAllDto)
        {
            return await _filmRepository.GetAllSkipTakeAsync(getAllDto);
        }

        public async Task<FilmModel>  Update(string id, UpdateDto updateDto)
        {
            // önce film bilgileri çekilip oradaki film bilgileri kısmını body kısmında gösterilebilir mi? 
            var result = await _filmRepository.Update(id, updateDto);
            // Kendi veritabanımızda film bilgisini güncelleyebilmek için 
            return result;
        }

        public void Delete(string id)
        {
            _filmRepository.DeleteRepo(id);
        }
    }
}