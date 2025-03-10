using DeepSeekUI.Application.Interfaces.Repositories.Common;
using DeepSeekUI.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DeepSeekUI.Infrastructure.Persistence.Repositories.Common
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : CommonEntity
    {
        protected readonly DeepSeekContext _context;
        public GenericRepository(DeepSeekContext context)
        {
            _context = context;
        }

        protected DbSet<T> Entities => _context.Set<T>();

        public async Task AddArrangeAsync(params T[] entity)
        {
            await Entities.AddRangeAsync(entity);
        }

        public async Task<T> AddAsync(T entity)
        {
            await Entities.AddAsync(entity);
            return entity;
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> Edit(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return Entities;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return Entities.Where(predicate);
        }

        public Task<T?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
