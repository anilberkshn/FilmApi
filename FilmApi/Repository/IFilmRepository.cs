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
        public Task<SearchByIdModel> GetByIdAsync(string imdbId);
        public Task<IEnumerable<SearchByIdModel>> GetAllAsync();
        public Task<IEnumerable<SearchByIdModel>> GetAllSkipTakeAsync(GetAllDto getAllDto);
        public Task<Guid> InsertAsync(SearchByIdModel searchByIdModel);
        public Task<SearchByIdModel> Update(string imdbId, UpdateDto updateDto);
        public Guid Delete(string imdbId);
        // public void SoftDelete(Guid id, SoftDeleteDto softDeleteDto);
        // public StatusDto ChangeStatus(Guid id, StatusDto statusDto);
        
        public Task<IEnumerable<SearchByIdModel>> GetManyTitle(string titleName);
    }
}