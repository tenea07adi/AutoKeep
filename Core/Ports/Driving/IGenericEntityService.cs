using Core.Entities.Abstract;
using System.Linq.Expressions;

namespace Core.Ports.Driving
{
    public interface IGenericEntityService<T> where T : BaseEntity
    {
        public Task<List<T>> GetAsync(Expression<Func<T, bool>>? expression = null);

        public Task<T?> GetAsync(int id);

        public Task AddAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(int id);
    }
}
