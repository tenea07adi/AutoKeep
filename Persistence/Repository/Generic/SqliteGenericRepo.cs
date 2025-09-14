using Core.Entities.Abstract;
using Core.Ports.Driven;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBase;
using System.Linq.Expressions;

namespace Persistence.Repository.Generic
{
    public class SqliteGenericRepo<T> : IGenericRepo<T> where T : BaseEntity, new()
    {
        protected readonly DataBaseContext _dbContext;

        public SqliteGenericRepo(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>>? expression = null)
        {
            if(expression == null)
            {
                expression = c => true;
            }

            return await _dbContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            var existingEntity = await GetAsync(entity.Id);

            if (existingEntity != null)
            {
                _dbContext.Set<T>().Update(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);

            if(entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
