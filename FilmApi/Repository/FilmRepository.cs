using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Database;
using Core.Database.Interface;
using Core.Model.RequestModel;
using FilmApi.Model.Entities;
using FilmApi.Model.RequestModels;
using MongoDB.Driver;

namespace FilmApi.Repository
{
    public class FilmRepository: GenericRepository<SearchByIdModel>,IFilmRepository
    {
        public FilmRepository(IContext context, string collectionName = "Film") : base(context, collectionName)
        {
        }

        public async Task<SearchByIdModel> GetByIdAsync(string imdbId)
        {
            var film = await FindOneAsync(x => x.ImdbId == imdbId);
            return film;
        }

        public async Task<IEnumerable<SearchByIdModel>> GetAllAsync()
        {
            return await FindAllAsync();
        }

        public async Task<IEnumerable<SearchByIdModel>> GetAllSkipTakeAsync(GetAllDto getAllDto)
        {
            return await GetManyAsync(getAllDto);
        }

        public async Task<SearchByIdModel> InsertAsync(SearchByIdModel searchByIdModel)
        {
            return await CreateAsync(searchByIdModel);
        }

        public async Task<SearchByIdModel> Update(string imdbId, UpdateDto updateDto)
        {
            var update = Builders<SearchByIdModel>.Update
                .Set(x => x.Title, updateDto.Title)
                .Set(x => x.Year, updateDto.Year)
                .Set(x => x.Rated, updateDto.Rated)
                .Set(x => x.Released, updateDto.Released)
                .Set(x => x.Runtime, updateDto.Runtime)
                .Set(x => x.Genre, updateDto.Genre)
                .Set(x => x.Director, updateDto.Director)
                .Set(x => x.Writer, updateDto.Writer)
                .Set(x => x.Actors, updateDto.Actors)
                .Set(x => x.Plot, updateDto.Plot)
                .Set(x => x.Language, updateDto.Language)
                .Set(x => x.Country, updateDto.Country)
                .Set(x => x.Awards, updateDto.Awards)
                .Set(x => x.Poster, updateDto.Poster)
                .Set(x => x.Dvd, updateDto.Dvd)
                .Set(x => x.BoxOffice, updateDto.BoxOffice)
                .Set(x => x.Production, updateDto.Production)
                .Set(x => x.Website, updateDto.Website)
                .Set(x => x.Response, updateDto.Response)
                ;

            Update(x => x.ImdbId == imdbId, update);
            return await GetByIdAsync(imdbId);
        }

        public void Delete(string imdbId)
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