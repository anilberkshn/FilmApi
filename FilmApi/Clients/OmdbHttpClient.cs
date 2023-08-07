using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Model.ErrorModels;
using FilmApi.Model.Entities;
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

        public async Task<FilmModel> GetCustomerByImdbId(string imdbId)
        {
            //Todo burada ki dönüşüm sorunu ile devam edilecek. 
            var response = await _httpClient.GetAsync($"http://www.omdbapi.com/?i={imdbId}&apikey=3d7170c0");
            var jsonconvert = JsonConvert.SerializeObject(response);
            string jsonResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                // var film = await response.Content.ReadFromJsonAsync<FilmModel>();
                var film = await response.Content.ReadFromJsonAsync<FilmModel>();
                return film;
            }
            else
            {
                throw new CustomException(HttpStatusCode.NotFound, "Film bulunamadı");
            }
        }
    }
}