using System.Collections.Generic;
using Core.Model.Entities;

namespace FilmApi.Model.Entities
{
    public class MovieSearchResult
    {
        public List<GenericDocument> Search { get; set; }
        public string TotalResults { get; set; }
        public bool Response { get; set; }
    }
}