using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Database;
using Core.Database.Interface;
using Core.Model.RequestModel;
using FilmApi.Model.Entities;
using FilmApi.Model.RequestModels;

namespace FilmApi.Repository
{
    public class FilmRepository: GenericRepository<SearchByIdModel>,IFilmRepository
    {
        public FilmRepository(IContext context, string collectionName = "Film") : base(context, collectionName)
        {
        }

        public Task<SearchByIdModel> GetByIdAsync(string imdbId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SearchByIdModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SearchByIdModel>> GetAllSkipTakeAsync(GetAllDto getAllDto)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> InsertAsync(SearchByIdModel searchByIdModel)
        {
            throw new NotImplementedException();
        }

        public Task<SearchByIdModel> Update(string imdbId, UpdateDto updateDto)
        {
            throw new NotImplementedException();
        }

        public Guid Delete(string imdbId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SearchByIdModel>> GetManyTitle(string titleName)
        {
            throw new NotImplementedException();
        }

        public Guid Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SearchByIdModel>> DeleteOrdersByCustomerId(string imdbId)
        {
            throw new NotImplementedException();
        }
    }
}