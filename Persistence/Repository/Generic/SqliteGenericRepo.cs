using Core.Entities.Abstract;
using Core.Ports.Driven;
using Persistence.DataBase;
using System.Linq.Expressions;

namespace Persistence.Repository.Generic
{
    public class SqliteGenericRepo<T> : IGenericRepo<T> where T : BaseEntity, new()
    {
        public async Task<List<T>> GetAsync(Expression<Func<T, bool>>? expression = null)
        {
            return await SqliteDataBase.DataBase.Table<T>().Where(expression).ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await SqliteDataBase.DataBase.GetAsync<T>(id);
        }

        public async Task AddAsync(T entity)
        {
            await SqliteDataBase.DataBase.InsertAsync(entity, typeof(T));
        }

        public async Task UpdateAsync(T entity)
        {
            await SqliteDataBase.DataBase.UpdateAsync(entity, typeof(T));
        }

        public async Task DeleteAsync(int id)
        {
            await SqliteDataBase.DataBase.DeleteAsync<T>(id);
        }
    }
}
