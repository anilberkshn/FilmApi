using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Model.ErrorModels;
using FilmApi.Model.Entities;
using FilmApi.Model.RequestModels;
using FilmApi.Model.ResponseModels;
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
              
               return film;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new CustomException(HttpStatusCode.NotFound, "The movie was not found");
            }
            else
            {
                throw new CustomException(response.StatusCode, "Api request error");
            }
        }
        
        public async Task<IEnumerable<SearchByTitle>> GetByTitle(string title)
        {
           var response = await _httpClient.GetAsync($"http://www.omdbapi.com/?s={title}&apikey=3d7170c0");
         
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var filmTitle = JsonConvert.DeserializeObject<TitleHttpClientResponse>(jsonResponse);
                 
                return filmTitle;// list dönecek atmış olduğumuz title sorgusu
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new CustomException(HttpStatusCode.NotFound, "The movie was not found");
            }
            else
            {
                throw new CustomException(response.StatusCode, "Api request error");
            }
        }
    }
} 



// var jsonConvert = JsonConvert.SerializeObject(response);
// var jsonConvertObject = JsonConvert.DeserializeObject(jsonConvert);
// string jsonResponse = await response.Content.ReadAsStringAsync();