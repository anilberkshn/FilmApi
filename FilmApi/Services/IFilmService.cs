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
        public Task<FilmModel> GetByIdAsync(string id);
        public Task<IEnumerable<FilmModel>> GetAllSkipTakeAsync(GetAllDto getAllDto);
        public Task<FilmModel> Update(string imdbId, UpdateDto updateDto);
        public void Delete(string id);
        public Task<IEnumerable<FilmModel>> GetByTitleAsync(SearchByTitleDto byTitleDto);

    }
}