using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Model.RequestModel;
using MongoDB.Driver;

namespace Core.Database.Interface
{
    public interface IGenericRepository<T>
    {
        public Task<T> CreateAsync (T record);
        public Task<IEnumerable<T>> FindAllAsync();
        public Task<IEnumerable<T>> FindAllSkipTakeAsync(GetAllDto getAllDto);
        public Task<IEnumerable<T>> GetManyAsync(GetAllDto getAllDto);
        public  Task<T> FindOneAsync(Expression<Func<T, bool>> expression);
        public void Update(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition);
        public void Delete(Expression<Func<T, bool>> expression);
        public void SoftDelete(Expression<Func<T, bool>> expression,UpdateDefinition<T> updateDefinition);

        public Task<IEnumerable<T>> GetByTitleAsync(SearchByTitleDto searchByTitleDto);
    }
}