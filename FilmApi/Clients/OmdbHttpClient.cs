using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Model.ErrorModels;
using FilmApi.Model.Entities;
using FilmApi.Model.RequestModels;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace FilmApi.Clients
{
    public class OmdbHttpClient : IOmdbHttpClient
    {
        private readonly HttpClient _httpClient;
        
        public OmdbHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
          
        }

        public async Task<FilmModel> GetByImdbId(string imdbId)
        {
            
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
        
        public async Task<SearchByTitleDto> GetByTitle(string title)
        {
           var response = await _httpClient.GetAsync($"http://www.omdbapi.com/?s={title}&apikey=3d7170c0");
         
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var filmTitle = JsonConvert.DeserializeObject<SearchByTitleDto>(jsonResponse);
                 
                return filmTitle;
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