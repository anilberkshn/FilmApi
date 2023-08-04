using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Model.ErrorModels;
using FilmApi.Model.Entities;

namespace FilmApi.Clients
{
    public class OmdbHttpClient: IOmdbHttpClient
    {
        private readonly HttpClient _httpClient;

        public OmdbHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FilmModel> GetCustomerByImdbId(string imdbId)
        {
            var response = await _httpClient.GetAsync($"?i={imdbId}&apikey=3d7170c0");
            if (response.IsSuccessStatusCode)
            {
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