using System.Collections.Generic;
using FilmApi.Model.Entities;

namespace FilmApi.Model.ResponseModels
{
    public class    TitleHttpClientResponse
    {
        public List<SearchByTitle> Search { get; set; }
        public string Response { get; set; }
    }
}