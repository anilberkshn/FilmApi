using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Model.ErrorModels;
using FilmApi.Model.Entities;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace FilmApi.Clients
{
    public class OmdbHttpClient : IOmdbHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;
        public OmdbHttpClient(HttpClient httpClient)//, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
           // _memoryCache = memoryCache;
        }

        public async Task<FilmModel> GetCustomerByImdbId(string imdbId)
        {
            // if (_memoryCache.TryGetValue(imdbId, out FilmModel cachedFilm))
            // {
            //     return cachedFilm;
            // }
            
            var response = await _httpClient.GetAsync($"http://www.omdbapi.com/?i={imdbId}&apikey=3d7170c0");
         
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var film = JsonConvert.DeserializeObject<FilmModel>(jsonResponse);
               // _memoryCache.Set(imdbId, film, TimeSpan.MaxValue); 
               return film;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new CustomException(HttpStatusCode.NotFound, "Film bulunamadı");
            }
            else
            {
                throw new CustomException(response.StatusCode, "Api istek hatası");
            }
        }
    }
} 



// var jsonConvert = JsonConvert.SerializeObject(response);
// var jsonConvertObject = JsonConvert.DeserializeObject(jsonConvert);
// string jsonResponse = await response.Content.ReadAsStringAsync();