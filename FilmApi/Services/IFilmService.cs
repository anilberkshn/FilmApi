using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model.RequestModel;
using FilmApi.Model.Entities;
using FilmApi.Model.RequestModels;

namespace FilmApi.Services
{
    public interface IFilmService
    {
        public Task<FilmModel> InsertAsync(FilmModel filmModel);
        public Task<IEnumerable<FilmModel>> GetAllAsync();
        public Task<FilmModel> GetByIdAsync(SearchByIdDto id);
        public Task<IEnumerable<FilmModel>> GetAllSkipTakeAsync(GetAllDto getAllDto);
        public Task<FilmModel> Update(SearchByIdDto imdbId, UpdateDto updateDto);
        public void Delete(SearchByIdDto id);
        public Task<IEnumerable<FilmModel>> GetByTitleRepoAsync(SearchByTitleDto byTitleDto);

    }
}