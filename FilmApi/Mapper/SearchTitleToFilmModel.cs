using System.Collections.Generic;
using System.Threading.Tasks;
using FilmApi.Model.Entities;
using FilmApi.Model.RequestModels;

namespace FilmApi.Mapper
{
    public class SearchTitleToFilmModel
    {
        public static async Task<FilmModel>  MapToSearchTitleToFilmModel(SearchByTitle searchByTitle)
        {
            return await Task.Run(() => new FilmModel()
            {
                Title = searchByTitle.Title,
                Year = searchByTitle.Year,
                ImdbId = searchByTitle.ImdbId,
                Type = searchByTitle.Type,
                Poster = searchByTitle.Poster
            });
        }
        // public static async Task<FilmModel>  MapToSearchTitleToFilmModel(List<SearchByTitle> searchByTitle)
        // {
        //     return await Task.Run(() => new FilmModel()
        //     {
        //         Title = searchByTitle.Title,
        //         Year = searchByTitle.Year,
        //         ImdbId = searchByTitle.ImdbId,
        //         Type = searchByTitle.Type,
        //         Poster = searchByTitle.Poster
        //     });
        // }
    }
}