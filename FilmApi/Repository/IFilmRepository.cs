using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model.RequestModel;
using FilmApi.Model.Entities;
using FilmApi.Model.RequestModels;

namespace FilmApi.Repository
{
    public interface IFilmRepository
    {
        public Task<FilmModel> InsertAsync(FilmModel filmModel);
        public Task<IEnumerable<FilmModel>> GetAllAsync();
        public Task<FilmModel> GetByIdAsync(SearchByIdDto id);
        public Task<IEnumerable<FilmModel>> GetAllSkipTakeAsync(GetAllDto getAllDto);
        public Task<FilmModel> Update(SearchByIdDto id, UpdateDto updateDto);
        public void DeleteRepo(SearchByIdDto id);
        // public void SoftDelete(Guid id, SoftDeleteDto softDeleteDto);
        // public StatusDto ChangeStatus(Guid id, StatusDto statusDto);
        
        public Task<IEnumerable<FilmModel>> GetByTitleRepoAsync(SearchByTitleDto byTitleDto);
    }
}