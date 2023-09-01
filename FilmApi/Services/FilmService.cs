using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Core.Model.ErrorModels;
using Core.Model.RequestModel;
using FilmApi.Clients;
using FilmApi.Mapper;
using FilmApi.Model.Entities;
using FilmApi.Model.RequestModels;
using FilmApi.Repository;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace FilmApi.Services  
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IOmdbHttpClient _omdbHttpClient;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<FilmService> _logger;
        
        public FilmService(IFilmRepository filmRepository, IOmdbHttpClient omdbHttpClient, IMemoryCache memoryCache, ILogger<FilmService> logger)
        {
            _filmRepository = filmRepository;
            _omdbHttpClient = omdbHttpClient;
            _memoryCache = memoryCache;
            _logger = logger;
        }

         public async Task<FilmModel> GetByIdAsync(string id)
        {
            if (_memoryCache.TryGetValue(id, out FilmModel cachedFilm))
            {
                _logger.LogInformation("The film was brought from the cache");
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

        public async Task<IEnumerable<FilmModel>> GetByTitleAsync(string byTitleDto)
        {
            var cachedFilms = _memoryCache.Get<IEnumerable<FilmModel>>(byTitleDto);
            if (cachedFilms != null )
            {
                return cachedFilms;
            }

            var dbFilms = await _filmRepository.GetByTitleAsync(byTitleDto);
            var filmModels = dbFilms.ToList();
           
            if (dbFilms != null && filmModels.Count() != 0) // Todo: sarı kısmı kaldırınca hata alıyor. 
            {
                var byTitleAsync = filmModels.ToList();
                _memoryCache.Set(byTitleDto, byTitleAsync, TimeSpan.FromDays(30));
                return byTitleAsync;
            }

            IEnumerable<SearchByTitle> httpResponse = await _omdbHttpClient.GetByTitle(byTitleDto);

            if (httpResponse == null)
            {
                var dbfilms = await _filmRepository.GetByTitleAsync(byTitleDto);
                if (dbfilms == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, "This title also could not find the movie.");
                }
                return dbfilms;
            }
          
            var searchByTitlesList = httpResponse.ToList();
            List<FilmModel> films = new List<FilmModel>();
            foreach (var searchByTitle in searchByTitlesList)
            {
                var httpResponseToFilmModel = await SearchTitleToFilmModel.MapToSearchTitleToFilmModel(searchByTitle);
                await _filmRepository.InsertAsync(httpResponseToFilmModel);
                _memoryCache.Set(byTitleDto, searchByTitlesList, TimeSpan.FromDays(30));
                films.Add(httpResponseToFilmModel);
            }
            
            return films;
        }

        public async Task<IEnumerable<FilmModel>> GetAllSkipTakeAsync(GetAllDto getAllDto)
        {
            return await _filmRepository.GetAllSkipTakeAsync(getAllDto);
        }

        public async Task<FilmModel> InsertAsync(FilmModel filmModel)
        {
            return await _filmRepository.InsertAsync(filmModel);
        }

        public async Task<IEnumerable<FilmModel>> GetAllAsync()
        {
            return await _filmRepository.GetAllAsync();
        }

       
        public async Task<FilmModel> Update(string id, UpdateDto updateDto)
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