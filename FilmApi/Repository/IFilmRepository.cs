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
        public Task<FilmModel> GetByIdAsync(string id);
        public Task<IEnumerable<FilmModel>> GetAllSkipTakeAsync(GetAllDto getAllDto);
        public Task<FilmModel> Update(string id, UpdateDto updateDto);
        public void DeleteRepo(string id);
        // public void SoftDelete(Guid id, SoftDeleteDto softDeleteDto);
        // public StatusDto ChangeStatus(Guid id, StatusDto statusDto);
        
        public Task<IEnumerable<FilmModel>> GetByTitleAsync(SearchByTitleDto byTitleDto);
    }
}