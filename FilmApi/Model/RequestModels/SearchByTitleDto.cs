namespace FilmApi.Model.RequestModels
{
    public class SearchByTitleDto
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string ImdbId { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }
    // Core request model mi burada mı daha doğru olur emin olamadım. 
}
//s fast
// "Title": "The Fast and the Furious",
// "Year": "2001",
// "imdbID": "tt0232500",
// "Type": "movie",
// "Poster": "https://m.media-amazon.com/images/M/MV5BNzlkNzVjMDMtOTdhZC00MGE1LTkxODctMzFmMjkwZmMxZjFhXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_SX300.jpg"