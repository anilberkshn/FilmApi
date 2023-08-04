using System.Collections.Generic;
using Core.Model.Entities;
using FilmApi.Model.Entities;

namespace FilmApi.Clients
{
    public class OmdbClientModel: GenericDocument
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string ImdbId { get; set; }
        
        public string Type { get; set; }
        public string Poster { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }//??
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
      
        public List<RaitingsModel> Ratings { get; set; }
        public string MetaScore { get; set; }//??
        public string ImdbRating { get; set; }//??
        public string ImdbVotes { get; set; }//??
        public string Dvd { get; set; }//??
        public string BoxOffice { get; set; }//??
        public string Production { get; set; }
        public string Website { get; set; }
        public bool Response { get; set; }
    }
}