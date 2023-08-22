using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FilmApi.Model.Entities;
using FilmApi.Model.RequestModels;

namespace FilmApi.Helper.Mapper
{
    public static class FilmToSearchTitleDto
    {
        public static async Task<SearchByTitleDto>  MapToSearchByTitleDto(FilmModel film)
        {
            return await Task.Run(() => new SearchByTitleDto
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