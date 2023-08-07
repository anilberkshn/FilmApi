using System;
using System.Collections.Generic;
using Core.Model.Config;
using Core.Model.Entities;
using FilmApi.Model.RequestModels;

namespace FilmApi.Model.Entities
{
    public class FilmModel : GenericDocument
    {
        public string Title { get; set; }
        public string Year { get; set; }
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
        public string Poster { get; set; }
        public List<RaitingsModel> Ratings { get; set; }
        public string MetaScore { get; set; }//??
        public string ImdbRating { get; set; }//??
        public string ImdbVotes { get; set; }//??
        public string ImdbId { get; set; }
        public string Type { get; set; }
        public string Dvd { get; set; }//??
        public string BoxOffice { get; set; }//??
        public string Production { get; set; }
        public string Website { get; set; }
        public bool Response { get; set; }
    }
}

/*  t jaws  search title

    "Title": "Jaws",
    "Year": "1975",
    "Rated": "PG",
    "Released": "20 Jun 1975",
    "Runtime": "124 min",
    "Genre": "Adventure, Mystery, Thriller",
    "Director": "Steven Spielberg",
    "Writer": "Peter Benchley, Carl Gottlieb",
    "Actors": "Roy Scheider, Robert Shaw, Richard Dreyfuss",
    "Plot": "When a killer shark unleashes chaos on a beach community off Cape Cod, it's up to a local sheriff, a marine biologist, and an old seafarer to hunt the beast down.",
    "Language": "English",
    "Country": "United States",
    "Awards": "Won 3 Oscars. 15 wins & 20 nominations total",
    "Poster": "https://m.media-amazon.com/images/M/MV5BMmVmODY1MzEtYTMwZC00MzNhLWFkNDMtZjAwM2EwODUxZTA5XkEyXkFqcGdeQXVyNTAyODkwOQ@@._V1_SX300.jpg",
    "Ratings": [
        {
            "Source": "Internet Movie Database",
            "Value": "8.1/10"
        },
        {
            "Source": "Rotten Tomatoes",
            "Value": "97%"
        },
        {
            "Source": "Metacritic",
            "Value": "87/100"
        }
    ],
    "Metascore": "87",
    "imdbRating": "8.1",
    "imdbVotes": "628,138",
    "imdbID": "tt0073195",
    "Type": "movie",
    "DVD": "14 Jun 2005",
    "BoxOffice": "$265,859,065",
    "Production": "N/A",
    "Website": "N/A",
    "Response": "True"
*/