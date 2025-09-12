using Core.Entities.Abstract;
using System.Linq.Expressions;

namespace Core.Ports.Driven
{
    public interface IGenericRepo<T> where T : BaseEntity, new()
    {
        public Task<List<T>> GetAsync(Expression<Func<T, bool>>? expression = null);

        public Task<T?> GetAsync(int id);

        public Task AddAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(int id);
    }
}
