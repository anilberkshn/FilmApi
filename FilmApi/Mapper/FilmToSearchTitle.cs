using System.Threading.Tasks;
using FilmApi.Model.Entities;
using FilmApi.Model.RequestModels;

namespace FilmApi.Mapper
{
    public static class FilmToSearchTitle
    {
        public static async Task<SearchByTitle>  MapToSearchByTitleDto(FilmModel film)
        {
            return await Task.Run(() => new SearchByTitle
            {
                Title = film.Title,
                Year = film.Year,
                ImdbId = film.ImdbId,
                Type = film.Type,
                Poster = film.Poster
            });
        }
    }
}