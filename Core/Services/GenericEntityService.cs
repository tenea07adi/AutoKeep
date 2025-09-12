using Core.Entities.Abstract;
using Core.Ports.Driven;
using Core.Ports.Driving;
using System.Linq.Expressions;

namespace Core.Services
{
    public class GenericEntityService<T> : IGenericEntityService<T> where T : BaseEntity, new()
    {
        private readonly IGenericRepo<T> _genericRepo;
        public GenericEntityService(IGenericRepo<T> genericRepo)
        {
            _genericRepo = genericRepo;
        }

        public async Task AddAsync(T entity)
        {
            await _genericRepo.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _genericRepo.DeleteAsync(id);
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>>? expression = null)
        {
            return await _genericRepo.GetAsync(expression);
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _genericRepo.GetAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            await _genericRepo.UpdateAsync(entity);
        }
    }
}
